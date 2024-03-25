#### A. How address translation works
(+) For simplified OS, it places the entire memory image in one chunk
![[Pasted image 20240216201517.png]]

#### B. Who performs address translation
(+) In this simple example, OS tells the hardware `the base (starting address)` and `the bound (total size of process)` value
(+) Memory hardware `Memory Management Unit (MMU)` caculates PA from VA using those value
```
Physical address = Virtual address + base
==> x = 128 + 32KB => 32896
```
(+) MMU also checks if address is beyond bound and the OS is not involved in every translation.

#### C. Role hardware in translation
(+) CPU provides privileged mode of execution.
(+) Instruction set has `privileged instructions` to set translation information (e.g., base, bound)
(+) Hardware (MMU) uses `base and bound` to perform translation on every memory access.
(+) MMU generates faults and traps to OS when access is illegal (e.g., VA is out of bound)

#### D. Role of OS in translation
(+) OS maintains free list of memory
(+) Allocates space to process during creation (and when asked) and cleans up when done
(+) Maintains information of where space is allocated to each process
(+) Sets address translation information (base and bound)
(+) Updates this information upon context switch
(+) Handles traps due to illegal memory access.

#### E. Segmentation
(+) Generalized `base and bounds` 
(+) Each segment of memory image placed separately, and multiple (base, bound) values are stored in MMU.
(+) Good for sparse address spaces.
(+) But variable sized allocation leads to external fragmentation, which is small holes in memory left between segments
![[Pasted image 20240216202343.png]]