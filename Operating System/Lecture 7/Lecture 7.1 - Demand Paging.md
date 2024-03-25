#### A. Is main memory always enough?
(+) Are all pages of all active processes in main memory?
==> Not necessary, with large address spaces, main memory can not afford all of them.
(+) OS uses a part of `disk` called `swap space` to store pages that are not in active use.
![[Pasted image 20240216205748.png]]

###### Page fault
(+) `Present bit` in page table entry indicates if a page of a process resides in memory or not
(+) When translating VA to PA, MMU reads present bit.
	(-) If page present in memory, directly accessed.
	(-) If not, MMU raises a trap to the OS -> `page fault`.

###### Handling page fault
(+) Once the hardware trap OS and moves CPU to kernel mode.
(+) OS fetches disk address of page and issues read to disk
	(-) OS keeps track of disk address (say, in page table)
(+) OS context switches to another process (as reading from disk is very long)
	(-) Current process is blocked and cannot run.
(+) When disk read completes, OS updates page table of process, and marks it as ready.
(+) When process scheduled again, OS restarts the instruction that caused page fault.
![[Pasted image 20240216210328.png]]

###### Page fault handling process
(+) If page fault is raised, OS will reload the instruction to get the page, however, what if there are no frames (in the PA) left for swapping the current page fault?
==> OS can swap out an existing page, then swap in the faulting page, but it will be too much work.

(+) OS may proactively swap out pages to keep a list of free pages handy for handling page fault, and which pages to swap out is decided by page replacement policy.

###### Page replacement policy
(+) Optimal: replace page not needed for longest time in future `not practical as we dont know the future`.
(+) FIFO: replace page that was brought into memory earliest `not efficient as it may be a popular page`.
(+) `Least Recently Used or Least Frequently Used (LRU/LFU)`: replace the page that was least recently used in the past.

###### Example
(+) Optimal policy: 3 frames for 4 pages (0,1,2,3))
	(-) First few accesses are cold (compulsory) misses.
![[Pasted image 20240219150458.png]]
(+) FIFO policy: Usually worse than optimal
	(-) Belady's anomaly: performance may get worse when memory size increases.
![[Pasted image 20240219150752.png]]

(+) LRU/LFU policy: equivalent to optimal in this simple example, but works well due to locality of references, meaning if a page has not been used recently, it is unlikely that it will be used in the future
![[Pasted image 20240219150853.png]]

###### How is LRU implemented
(+) OS is not involved in every memory access - how does it know which page is LRU?
(+) MMU sets a bit in PTE (`accessed` bit) when a page is accessed.
(+) OS periodically looks at this bit to estimate page that are active and inactive
(+) TO replace, OS tries to find a page that does not have access bit set
==> May also look for page with `dirty` bit not set (to avoid writting to disk)