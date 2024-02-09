#### A. Memory virtualization problem
(+) Guest's `RAM` is actually memory of the userspace hypervisor process running on the host, which is mapped to host memory by the host's page table.
==> Whenever, the guest needs to access its virtual address, the hypervisor needs to translate from the virtual address to the host physical address and vice versa.
![[Pasted image 20240209185936.png]]

`NOTE:`
(+) Guest page table has `GVA -> GPA` mapping, each guest OS thinks it has access to all RAM starting at address 0
(+) VMM/Host has `GPA -> HPA` mapping, the GPA is set up by the VMM on the host's userspace memory, or in other words, the GPA is set up on `Host Virtual Address`.
==> The host itself knows the mapping from `Host Virtual Address(HVA)  -> Host Physical Address(HPA)`, therefore, the host OS knows the mapping from `GPA -> HPA`.
====> That is, guest `"RAM"` pages are userspace process pages that are distributed across host memory and host OS knows the physical locations of the guest's `"RAM"` pages.

`THIS RAISES A QUESTION OF WHAT PAGE TABLE SHOULD THE MEMORY MANAGEMENT UNIT USE? THE GVA -> GPA or THE GPA -> HPA?`.

(+) `Shadow paging`: VMM creates a combined mapping `GVA -> HPA` and `MMU` is given a pointer to this page table => This is used in `VMware workstation (full virtualization)`.

(+) `Extended page tables (EPT)`: MMU hardware is aware of virtualization, it use `two pointers` for `two separate page tables` and figures out the mapping of `GVA -> HPA` => This is used in `QEMU/KVM` for `hardware-assisted virtualization.

#### B. Extended page tables
(+) Page table walk by the MMU: Start walking guest page table using GVA.
(+) Guest's `Point Table Entry (PTE) meaning for each walks` gives `GPA`, however, modifying `GPA` alone would not work.
(+) Using GPA, the host will walk its page table (containing the mapping of GPA -> HPA) to find the corresponding HPA, then it will access the memory page of the HPA, the continue the next walk.
(+) Every step in guest page table walk requires walking N-level host page table
(+) `N-level` page tables in guest/host result in page table walk of `NxN` memory accesses.

![[Pasted image 20240209193926.png]]

#### C. Shadow page tables.
(+) VMM construct a combined mapping of `GVA->GPA` and `GPA->HPA` to create a single mapping table and whenever the Guest changes page table, the changes is trapped to VMM, shadow entry is updated.

![[Pasted image 20240209194056.png]]