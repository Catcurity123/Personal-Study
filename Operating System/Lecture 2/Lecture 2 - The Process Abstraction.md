#### A. OS provides process abstraction
(+) When we run an exe file, the OS creates a process (which is a `running program`).
(+) The OS timeshares CPU across multiple processes: `virtualizes the CPU`
(+) OS has a `CPU scheduler` that p`icks one of the many active processes to executes` on a CPU
	(-) `Policy`: which process to run.
	(-) `Mechanism`: how to `context switch` between processes

#### B. What constitutes a process?
(+) Must have a `unique identifier (PID)`, to identify itself from other processes.
(+) Must have `memory image`
	(-) Code & data (static): `Fixed data` that has been compiled.
	(-) Stack & heap (dynamic):  `Modifiable data` from instructions.
(+) Must have `CPU context`: `registers`
	(-) `Program counter`: register points to the current line of code.
	(-) `Current operands`: register containing current data (that could be currently modifying).
	(-) `Stack pointer`: register points to the current address of the stack.
(+) Must have `file descriptors`:
	(-) Pointers to open files and devices: to track `Standard Output` (when writing on the screen), `Standard Input` (when typing on the keyboard), `Standard Error` (To display error when happend).

==> All of these things correspond to the `state of the process` on the CPU as `it is being executed`.

#### C. How does OS create a process?
(+) Allocates memory and creates memory image:
	(-) Loads code, data from disk exe.
	(-) Creates runtime stack, heap.
(+) Opens basic files: STD IN, OUT, ERR.
(+) Initializes CPU registers:
	(-) PC points to first instruction.

#### D. States of a process
(+) `Running`: currently running on CPU.
(+) `Ready`: waiting to be scheduled.
(+) `Blocked`: suspended, not ready to run
	(-) Why? ==> Waiting for some events, e.g., process isuues a read from disk which takes time.
	(-) When is it unblocked? Disk issues an interrupt when data is ready.
(+) `New`: being created, yet to run.
(+) `Dead`: Terminated.
![[Pasted image 20240209213401.png | 400]]![[Pasted image 20240209213600.png]]
#### E. OS Data Structure
(+) OS maintains a data structure (e.g., list) of all active processes.
(+) Information about each process is stored in a process control block (PCB)
	(-) Process Identifier
	(-) Process state
	(-) Pointers to other related processes (parent)
	(-) CPU context of the process (saved when the process is suspended)
	(-) Pointers to memory locations
	(-) Pointers to open files
