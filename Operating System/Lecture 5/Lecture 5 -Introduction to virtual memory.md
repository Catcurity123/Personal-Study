#### A. Why virtualize memory?
(+) Because real view of memory is messy as earlier, memory had only code of one running process (and OS code).
(+) Now, multiple active `processes timeshare CPU` making the memory of many processes must be in memory, and it `may not in one block` meaning `non-contiguous`. Therefore, we need to hide this complexity from the user.
![[Pasted image 20240216192237.png | 250]]
==> `Virtual Address Space`

#### B. Abstraction from Virtual Address Space
(+) Virtual address space: every process assumes it has access to a large space of memory from address 0 to a MAX, `assuming in this context` means we assign `an amount of space in the physical memory` for the process to operate in.
(+) Contains program `code` (and `static data`), `heap` (`dynamic allocations, for pointer value to retain after function`), and `stack` (used to store data, variable during function calls).
	(-) Stack and heap grow during runtime as the instruction in the `Program code` is executed.
(+) CPU issues loads and stores to virtual addresses.

###### So how is this abstraction done, meaning how the physical addresses is virtualized to these virtual addresses?
(+) Address translation from `virtual address (VA)` to `physical address (PA)`
	==> The CPU issues `loads/stores` to VA but memory hardware accesses PA
(+) OS allocates memory and tracks location of processes
(+) The translation from VA -> PA is done by memory hardware called `Memory Management Unit (MMU)`
![[Pasted image 20240216194608.png]]

###### Example: Paging
(+) OS divides virtual address space into `fixed size pages`, physical memory into `frames`.
(+) To allocate memory, a `page` is mapped to a free physical `frame`.
(+) Page table stores mapping from virtual page number to physical number for a process and the `MMU` has access to page tables and uses it to translate `VA to PA`.
![[Pasted image 20240216195135.png]]
#### C. How can a user allocate memory
(+) OS allocates a set of pages to the memory image of the process
(+) Within this image:
	(-) `Static/global variables` are allocated in the executable
	(-) Local variable of a function on stack.
	(-) Dynamic allocation with `malloc` on the heap.

#### D. Memory allocation of system calls
(+) `malloc` implemented by C library, which is the algorithms for efficient memory allocation and free space management.
(+) To grow heap (which is a privileged actionn), `libc` uses the `brk/sbrk` system call.
(+) A program can also allocate a page sized memory using the `mmap()` system call.

#### E. How does the OS allocate memory
(+) OS also needs memory for its data structures.
(+) For large allocations, OS allocates a page, for smaller allocations, OS use various memory allocation algorithm (as `libc` and `malloc` is not available in kernel space)
