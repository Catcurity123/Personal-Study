#### A. What does VMM do
(+) Multiple VMs running on a PM - multiplex the underlying machine, similar to how OS multiplexes processes on CPU.
(+) `The problem is:` Guest OS expects to have unrestricted access to hardware, runs privileged instructions, unlike user processes. But one guest cannot get access, must be isolated from other guests.

#### B. Trap and emulate VMM (1)
(+) All CPUs have multiple privilege levels, normally user process in ring 3 and OS in ring 0. Guest OS must be protected from guest apps, but cannot be fully privileged like host OS/VMM, so it Guest OS can run in an intermediate ring like ring 1?

==> `Trap and emulate VMM` means that guest OS runs at lower privilege level than VMM, traps to VMM for privileged operation but still has a higher privilege than the guest App.

![[Pasted image 20240122190956.png]]

(+) Therefore, whenever an application in the guest OS makes a privileged call, it will be redirect to the VMM, the VMM will do the work on behalf of the guest OS (emulate) and then return the result to the guest OS and the guest OS return to the guest application.
![[Pasted image 20240122191414.png | 600]]

`Problem with trap and emulate`:
(+) Guest OS may realize it is running at lower privilege level and can get crashed. 
(+) Some x86 instructions which change hardware state run in both privileged and unprivileged modes making the behaviors different when guest OS in ring 0 and in ring 1. As Oses are not developed to run at a lower privileged level.

`An example for this is`:
(+) `Eflags` register is a set of CPU flags, IF (interrupt flag) is one of flags in this registers and it indicates if interrupts is on/off. `popf` is an instruction in x86, which pops the values on top of the `stack` and sets `eflags`, this command can be run in both privileged and nonprivileged mode.
	(-) If it is run in privileged mode, all the flags will be set to `Eflags register` normally, however, if it is run in lower privileged only some flags are set and in this case `IF` will not be set.
==> So `popf` is a sensitive instruction and behaves differently when executed in different privilege levels.

#### C. Popek Goldberg theorem
(+) `Sensitive instruction` means changes in hardware state and `Privileged instruction` means runs only in privileged mode. Therefore, `Popek Goldberg theorem states that` in order to build a VMM efficiently via trap-and-emulate method, `sensitive instruction should be a subset of privileged instructions`.
![[Pasted image 20240122192927.png]]

==> x86 does not satisfy this criteria, so trap and emulaate VMM is not possible.
![[Pasted image 20240122193003.png]]

#### D. Techniques to virtualize x86
(+) `Paravirtualization`: rewrite guest OS code to be virtualizable. So the guest OS wont invoke privileged operations, makes `hypercalls` to VMM (just like processes make `systemcalls` to the OS). An example for this is `Xen hypervisor`.
==> This means that OS source code needs to be changed and cannot work with unmodified OS. 

(+) `Full virtualization`: Change the CPU instruction of the guest OS to be virtualizable. `Sensitive instructions` will then be translated to trap to `VMM`. This means Dynamic (on the fly) binary translation, so it will works with unmodified OS. An example for this is `VMWare workstation`.
==> This method presents a higher overhead than paravirtualization.

(+) `Hardware assited virtualization`: CPU is now `awared` of virtulization and it has a special `Virtual Machine Extensions (VMX) mode`. x86 has 4 rings on non-VMX root mode, another 4 rings in VMX mode. Examples are `KVM/QEMU` in Linux.

![[Pasted image 20240122194748.png]]

==> In this method, the guest VM is hosted on an application, and we will enter `VMX mode` to run VM, in this `VMX mode` there are rings for the VMs to run and it is not awared that itself is a VM. Whenver the guest app makes privileged calls to guest OS, based on the nature of the calls (some will be executed by the guest OS some will need to be traps to VMM), it will exit to trap to VMM and the VMM will emulate the process for the guest OS.

So above are the methods to virtualize the CPU meaning how to get multiple guests to run on the same OS. We will need to understand `memory virtualization` as well.

#### E. Memory Virtualization
(+) So the guest has a `Guest Virtual Addresses (GVA)` which is comprised from multiple `Guest Physical Addresses (GPA)`. However, it is what the guest thought it has, in reality, these so-called `physical addresses` that the guest thought it has is the `allocated addresses` on the Host or `Host Physical Addresses (HPA)`.

![[Pasted image 20240122195615.png]]

###### Techniques for memory virtualization
(+) Guest page table has a `GVA -> GPA` mapping meaning the mapping of guest virtual address to guest physical address. And each guest OS thinks of this as its own RAM.
(+) VMM/Host OS has an `GPA -> HPA` mapping meaning the mapping of guest physical address to host physical address. Guest `RAM (or what it though it has)` pages are distributed across host memory.
==> The `problem` arises as the `Memory Management Unit` does not know which page table it should use. And techiniques are required to solve this.

###### 1. Shadow paging
(+) VMM creates a combined mapping of `GVA -> HPA` and `MMU` is give a pointer to this page table. VMM will track changes to guest page table and updates shadow page table.

###### 2. Extended page tables (EPT)
(+) MMU hardware is aware of virtualization, it takes pointers to two separate page tables. Meaning that the address translation walks both page tables making it more efficient than `Shadow paging`. However, the hardware device is required to support this.

#### F. I/O Virtualization
(+) Guest OS needs to access I/O devices, but we cannot give full control of I/O to any guest OS. So there also needs to be techniques for I/O virtualization:

###### 1. Emulation:
(+) Guest OS I/O operations trap to VMM, emulated by doing I/O in VMM/host OS.

###### 2. Direct I/O or device passthrough
(+) Assign a slice of device directly to each VM.