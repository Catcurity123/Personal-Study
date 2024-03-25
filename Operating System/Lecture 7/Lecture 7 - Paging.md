#### A. What is paging
(+) Allocate `VA` memory in fixed size chunks called `pages`. and the `PA` is allocate in fixed size `page frames`.
==> This is done to avoid `external fragmentation` (in the PA), but it has`internal fragmentation` (in the VA) when the program asks for space that is smaller than the size of a page.
![[Pasted image 20240216203034.png]]

#### B. Page table
(+) It is a `per-process data structure`, meaning it exists for each process, to help `VA -> PA` translation.
(+) Array stores mappings from `Virtual page number (VPN)` to `Physical frame number (PFN)`. E.g., VP 0 -> PF 3, VP 1 -> PF 7
(+) For example, if we have a simple process requiring 4 `pages` with its corresponding 4 `page frames`
![[Pasted image 20240216203454.png]]
(+)  MMU has access to page table and uses it for address translation.
(+) OS updates page table upon context switch

#### C. Page table entry (PTE)
(+) Simplest page table: linear page table
(+) Page table is an array of page table entries, one per virtual page
(+) VPN is indexed into this array
(+) Each PTE contains PFN (Physical frame number) and other bits:
	(-) Valid bit: is this page used by process?
	(-) Protection bits: read/write permissions?
	(-) Present bit: is this page in memory?
	(-) Dirty bit: has this page been modified
	(-) Accessed bit: has this page been recently accessd.
![[Pasted image 20240216204101.png]]

#### D. What happens on memory access?
(+) CPU request code or data at a virtual address.
(+) Before this can be done, the MMU must translate VA to PA
	(-) First, access memory to read page table entry.
	(-) Translate VA to PA
	(-) Then, access memory to fetch code/data.
(+) Paging adds overhead to memory access, a `cache for VA-PA mappings` can somewhat solve this.

###### Translation Lookaside Buffer (TLB)
(+) A cache of recent VA -> PA mappings
(+) When request appears, the MMU first look up TLB.
	(-) If TLB hits, PA can be directly used
	(-) If TLB miss, then MMU performs additional memory accesses to `walk` page table
(-) TLB misses are expensive and it can become invalid on context switch and change of page table.

###### How are page tables stored in memory
![[Pasted image 20240216204817.png]]

###### Multi-level page tables
(+) Page tables are spread over many pages in the PA, and there is a page directory to tracks the exact location of each page => making the `walk` and storage of such pages much more efficent.

(+) And depending on how large the page table is, we may need more than 2 levels, for example, 64-bit architectures may need 7 levels.
(+) For address translation in such table:
	(-) First few bits of VA to identify outer page table entry.
	(-) Next few bits to index into the next level of PTEs.
![[Pasted image 20240219144815.png]]

==> In cast the TLB miss, multiple access to memory required to access all the levels of page table