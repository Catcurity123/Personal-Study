#### A. What happens when you run a program? 
(+) A compiler translate high level programs into an executable(`.c` to `a.out`)
(+) The exe contains instructions that the CPU can understand, and data of the program (all numbered with addresses).
(+) Instructions run on CPU: hardware implements an `instruction set architecture (ISA)`
(+) CPU also consists of a few registers, for example, the `Pointer to current instruction (program counter or PC)`, `Operands of instructions`, `memory addresses`

![[Pasted image 20240209205837.png | 500]]

#### B. What does the OS do?
(+) OS `manages program memory`, loads program executable (code, data) from `disk` to `memory`.
(+) OS `manages CPU`, initializes `program counter` (PC) and other `registers` to begin execution.
(+) OS `manages external devices`, I/O devices to support read/write files from disk.

###### 1. How does the CPU manage the CPU?
(+) OS provides the process abstraction.
	(-) `Process`: a running program.
	(-) OS creates and manages processes.
(+) We might run different proceses at the same time, so the `OS virtualizes the CPU` and effectively gives each process the `illusion of having a complete CPU to itself`.
(+) This is done by timesharing CPU between processes and enables coordination between processes.

###### 2. How does the CPU manage the memory?
(+) OS manages the memory of the `process`, including `code, data, stack, heap etc`
(+) Each process thinks it has a dedicated memory space for itself, numbers code and data starting from 0 `(virtual addresses)`.
(+) OS abstracts out the details of the actual placement in memory, translates from `virtual addresses` to actual `physical addresses`.

###### 3. How does the CPU manage the devices?
(+) OS has code to manage disk, network card, and other external devices called `device drivers`
(+) Device driver talks the language of the hardware devices
	(-) `Issues instructions` to devices (for example, fetching data from a file)
	(-) Responds to `interrupt events` from devices (user has pressed a key on keyboard)
(+) Persistent data organized as a `filesystem` on disk

###### 4. Design goals of an operating system
(+) Convenience, abstraction of hardware resources for user programs.
(+) Efficiency of usage of CPU, memory.
(+) Isolation between multiple processes.

#### C. History of Operating Systems
(+) Started out as a library to provide common functionality across program.
(+) Later, evolved from `procedure call` to `system call`.
	(-) When a `system call` is made to run OS code, the CPU `executes at a higher privilege level`.
(+) Evolved from running a single program to multple processes `concurrently`.