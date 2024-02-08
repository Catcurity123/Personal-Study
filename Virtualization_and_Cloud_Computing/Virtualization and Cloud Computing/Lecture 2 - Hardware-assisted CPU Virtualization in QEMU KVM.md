#### A. Hardware-assisted Virtualization
(+) Modern technique, after hardware support for virtualization introduced in CPUs
	(-) Original x86 CPUs did not support virtualization
	(-) `Intel VT-X` or `AMD-V` support is widely available in modern systems.
	(-) Special CPU mode of operation called `VMX mode` for running VMs
(+) Many hypervisors use this H/W, e.g., `QEMU/KVM` in Linux

#### B. Libvirt and QEMU/KVM
(+) When we install QEMU/KVM on Linux, `libvirt` is also installed
	(-) A set of tools manage hypervisors, including QEMU/KVM
	(-) A daemon runs on the system and communicate with hypervisors
	(-) Exposes an API using which hypervisors can be managed, VM created etc.
	(-) Commandline tool (`virsh`) and GUI (`virt-manager`) use this API to manage VMs
![[Pasted image 20240208184225.png]]

#### C. QEMU architecture
(+) QEMU is userspace process
(+) KVM exposes a dummy device, QEMU talks to KVM via `open/ioctl syscalls`
(+) Allocates memory via mmap for guest VM physical memory
(+) Creates one thread for each virtual CPU (VCPU) in guest
(+) Multiple file descriptors to `/dev/kvm`(one for QEMU, one for VM, one for VCPU and so on) 
(+) Host OS sees QEMU as a regular `multi-threaded process`
![[Pasted image 20240208184622.png | 300]]
#### D. QEMU operation
![[Pasted image 20240208185522.png]]

![[Pasted image 20240208190623.png]]

#### E. VMX mode
(+) `Special CPU instructions` to enter and exit VMX mode
	(-) VMLAUNCH, VMRESUME invoked by KVM to enter VMX mode
	(-) VMEXIT invoked by guest OS to exit VMX mode

(+) On VMX entry/exit instructions, `CPU switches context between host OS to guest OS`
	(-) Page tables (address space), CPU register values etc switched
	(-) Hardware manages the mode switch

(+) Where is CPU context stored during mode switch
	(-) Cannot be stored in host OS or guest OS data structure alone 
	(-) VMCS (VM control structure), also called VMCB (VM control block)

#### F. VM control structure (VMCS)
(+) VMCS is a common memory area accessible in both modes, one VMCS per VM (KVM tells CPU which VMCS to use)
(+) VMCS stores `Host CPU contest (RAM address)` (store when launching VM, restored on VM exit) and `Guest CPU contest` (store when VM exit, restore when VM is run).
![[Pasted image 20240208194157.png | 400]]
(+) It also stores `Guest entry/execution/exit control area` and `exit information`.
(+) VMCS information is exchanged with QEMU via `kvm_run` structure
==> VMCS is only accessible to KVM in kernel mode, not to QEMU userspace.

#### G. VMX mode execution
(+) Restrictions on guest OS execution, configurable exits to KVM, guest OS exits to KVM on certain instruction (I/O device access)
(+) There is no hardware access to guest OS, emulated by KVM
	(-) Guest OS usually exits on interrupts (interrupts handled by KVM, assigned to the appropriate host or guest OS)
	(-) KVM can inject virtual interrupts to guest OS during VMX mode entry.
(+) All of the above controlled by KVM via VMCS.
==> Mimics the `trap-and-emulate architecture` with hardware support => Guest OS runs in a (special, faked) ring 0, but trap-and-emulate achieve.

###### Overview
![[Pasted image 20240208195424.png | 600]]
###### Host View
![[Pasted image 20240208195624.png]]

###### Summary
![[Pasted image 20240208195722.png]]