#### But why?
(+) Guest OS cannot get full access to I/O devices, VMM must share I/O device access guests.
(+) There are 2 ways to virtualize I/O devices: `Emulation` or `Direct I/O, device passthrough`

#### A. How the communication happends between OS and device?
(+) Device memory exposed as registers (command, status, data etc.)
=> When a `Write operation` happens it is triggered using a command, the data is written to the data registers, and the status indicates the completeness of that access and the data is written into the memory and subsequently the storage.

#### B. QEMU/KVM I/O Handling

![[Pasted image 20240209195846.png]]

(+)  A separate I/O threads to handle I/O ops, VCPU threads do not block for I/P Guest VM resumes and can context switch to another process.


#### C. Full Virtualization VMM Architecture
![[Pasted image 20240209200256.png]]

#### D. Device passthrough or Direct I/O
(+) More efficient than device emulation, for example `SR-IOV (Single Root IO Virtualization)` in network device.
	(-) Network card has one `physical function (PF)` and many `virtual function (VFs)`.
	(-) PF managed by host OS, each VF is then assigned to one guest VM.
	(-) Each VF is like a separate NIC, and is bound to a guest VM
	(-) Packets destined to the MAC address of VM are switched to corresponding VF.
![[Pasted image 20240209200613.png]]

(+) SR-IOV communicates directtly with device driver in guest OS
	(-) Packets do not go to the host OS stack at all
	(-) Packets switched at Layer-2 using VM virtual device's MAC address
	(-) Packets DMA'ed directly into guest VM memory, host OS not involved.
	(-) But, interrupts may still cause VM exit (interrupt can be for host too)

==> `This raises a concern` as when the guest driver provides `Direct Memory Address buffers` to VF, it can only provide `Guest Physical Addresses (GPA)` of the buffer, but NIC cannot access the DMA buffer memory using `GPA` alone, it needs the `HPA`.
=====> SR-IOV capable NICs solves this by having an inbuilt MMU called `IOMMU` to translate from GPA -> HPA.