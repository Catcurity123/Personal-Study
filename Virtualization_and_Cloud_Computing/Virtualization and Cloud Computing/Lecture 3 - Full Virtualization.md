#### What is the concern and motivation of full virtulization
(+) x86 and other hardware lacked virtualization support => but cloud computing increased demand for virtualization
(+) VMWare workstatios was the first to solve the problem of virtualization existing OS on x86 using `type 2 hypervisor based on trap-and-emulate approach`.
(+) The key idea is: `dynamic` (on a need basis) `binary` (not source) `translation` of OS instruction
==> Problematic OS instructions (sensitive instruction that runs on both priviledged and unpriviledged but with different result) are `translated` before execution.

###### Note
(+) Subsequently, hardware support for virtualization (QEMU/KVM) is more preferred.
	==> `Binary translation`is higher overhead than `hardware-assisted virtualization`
	==> Full-Virtual is then used when hardware support is not available.

#### A. Full-Virtual VMM architecture
![[Pasted image 20240208204906.png]]

(+) Full-Virtual solves the problem of Guest OS needs to make priviledged calls by `world switch from host context to VMM context` and `Traps priviledged actions made by Guest OS to VMM` using `binary translation of needed instructions`. So the Guest OS still made `priviledged instructions` but it is `translated by the VMM` into instruction that is `suitable to Guest's context` not the `Host's context` => The problem of sensitive instruction not being in the priviledged instruction is solved.

#### B. Host and VMM contexts
(+) Each context has separate page tables, CPU registers, IDTs and so on.
(+) VMM context: VMM occupies top `4MB` of address space
(+) Memory page containing code/data of world switch mapped in both contexts.
	==> Host/VMM context is `saved/restored` in this special `cross` page by VMM
![[Pasted image 20240208210557.png | 500]]

###### Understand the difference between QEMU/KVM and Full-Virtual
`1. Where is context saved?`
(+) Full-Virtual: Common cross page mapped into both host and guest address spaces
(+) KVM: Common Memory (VMCS) accessible by CPU in both contexts via special instructions

`2. Privilege level of guest OS?`
(+) Full-Virtual: Guest OS runs in ring 1 (lower privilege). Instructions that do not run correctly at lower privilege level are suitably translated to trap to VMM.
(+) KVM: Guest OS runs in VMX ring 0. Some privileged instructions trigger exit to KVM

`3. How to trap to VMM?`
(+) Full-Virtual: VMM is located in top 4MB of guest address space, guest OS traps to VMM for privileged ops. World switch to host if VMM cannot handle trap in guest context.
(+) KVM: VMM is not in guest context, guest  traps to VMM in host via VM exit.

#### C. Binary Translation
(+) Guest OS binary is translated instruction-by-instruction and stored in `translation cache (TC)`
	(-) Part of VMM memory
	(-) Most code stays the same, unmodified.
	(-) OS code modified to work correctly in ring 1.
	(-) Sensitive but unprivileged instruction modified to trap

(+) Guest OS code executes from TC in ring 1
(+) Priviledged OS code traps to VMM
	(-) I/O, set IDT, set CR3, other privileged ops
	(-) Emulated in VMM context or by switching to host
	(-) VMM sets sensitive data structures like IDT (maintain `shadow copies`) => Meaning guest OS has a copy of what it though to be its `address table` and want to make changes to that, changes in this context is priviledged and therefore will be map to a copies maintain by VMM, VMM will map the Guest OS's table with its `shadow copies`.

![[Pasted image 20240208212748.png | 180]]

#### D. Dynamic binary translation
(+) VMM translator logic (ring 0) translates guest code one `basic block` at a time to produce a `compiled code fragment (CCF)` => `basic block = sequence of instructions until a jump/return`.
(+) Once CCF is created, move to ring 1 to run translated guest code.
(+) Once CCF ends, `call out` to VMM logic, compute next instruction to jump to, translate, run CCF, and so on.
(+) If next CCF present in TC already, then directly jump to it without invoking VMM translator logic => this optimization is called `chaining`.
![[Pasted image 20240208213550.png | 400]]
#### E. Use of segmentation for protection
(+) Paging protects user code from kernel code via bit in page table entry
	(-) Segment are `flat` meaning that it is divided into `contiguous, non-overlapping` segments. Each segment represents a logical division of memory and can be used to store different types of data or code.
	(-) Separate flat segments for user and kernel modes.

(+) However, In a computer system, paging is a memory management technique that allows the physical memory to be divided into fixed-size blocks called pages. The virtual memory used by processes is also divided into the same-sized blocks called virtual pages. The mapping between virtual pages and physical pages is maintained in a data structure called the page table.
==> One of the key benefits of paging is the protection it provides between user code and kernel code. This protection is achieved through the use of a specific bit in the page table entry called the `user/kernel` or `supervisor` bit.
==> The table belongs to user mode is set with the `user/kernel bit` clear, and vice versa so that the `code segment/data segment (CS/DS)` is protected.
==> `This raised a problem in virtualization:` As there are 3 entities `the user`, `the kernel`, and `the VMM` all of which needs to be separted, so a single bit is not enough.

(+) Segmentation is used to protect VMM from guest (which is in the user processes).
	(-) Flat segments truncated to exclude VMM.
	(-) CS of guest OS (ring 1) points to VMM
	(-) VMM (ring 0) segments point to top 4MB.

![[Pasted image 20240208220024.png | 500]]

#### F. GS segment 
(+) Sometimes, translated guest code (ring 1) needs to access VMM data structure like saved register values, program counters and so on.
(+) In such cases, memory accesses are rewritten to use the GS segment, e.g., virtual address `GS:someAddress`
(+) GS register points to the 4MB VMM area in ring 1 ==> Ensures that the translated guest OS code can selectively access VMM data structures.
(+) Original guest code that uses GS (which is rare) is rewritten to use another segment like %fs

#### G. Summary
![[Pasted image 20240208221322.png]]

