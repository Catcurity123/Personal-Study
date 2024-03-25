#### A. How does the Process Execution happen?
(+) To make a program run as fast as one might expect, not surprisingly OS developers came up with a technique, which we call `limited direct execution`. The “direct execution” part of the idea is simple: just run the program directly on the CPU.

(+) Thus, when the OS wishes to start a program running, it creates a process entry for it in a process list, allocates some memory for it, loads the program code into memory (from disk), locates its entry point (i.e., the main() routine or something similar), jumps to it, and starts running the user’s code.
![[Pasted image 20240211191548.png]]

(+) This simple approach, however, presents a lot of problems that require solutions:
	(-) `Problem 1`: If we just run a program, how can the OS make sure the program doesn’t do anything that we don’t want it to do, while still running it efficiently?
	(-) `Problem 2`: When we are running a process, how does the operating system stop it from running and switch to another process, thus implementing the time sharing we require to virtualize the CPU?

###### Problem 1: Resitrcted Operations
(+) Running the program natively on the hardware CPU is as quickly as expected, but `what if the process wishes to perform some kind of restricted operation?` We can not give the process complete privilege nor can we disregard its request.
==> The solution for this problem is two modes: `usermode` and `kernelmode`.
`Usermode` has code that is restricted in what it can do, `kernelmode` runs in higher privilege that has access to mostly all the function of the system.

(+) Whenever the `usermode` wishes to have privilege access, it will make a `system call` to the `kernel mode`, the system will then switch to `kernel mode` to perform the requested action. Upon completion, the system will switch back to `usermode`.

(+) Therefore, when in `usermode` and wishes to perform privilege actions, the system will make `system call` to `kernelmode`, the system will execute a special `trap instruction` that will `jumps into kernel` (jump into the memory allocated for kernel) and `raises the privilege`. Then the system can do what it wishes if it is allowed with the appropriate permissions. Upon completion, the `kernel` will execute `return-from-trap instruction` to return back to the `usermode`. 

(+)Another problem is `how can the kernel return back to the usermode?` ==> The processor will push the `program counter`, `flags`, and a few other `registers` onto a `per-process kernel stack`; the return-from-trap will pop these values off the stack and resume execution of the usermode program.

(+) Another problem is `at the kernelmode, how does the trap instruction know which code to run inside the OS`. As the calling process can not specify an address to jump around inside kernelmode, doing that would allow the program to jump anywhere into the kernel which is `a very bad idea`.
==> The kernel does so by setting up a `trap table` at `boot time`. When the machine boots up, it does so in `privileged (kernel) mode`, and thus is free to configure machine hardware as need be. One of the first things the OS thus does is to tell the hardware what code to run when certain exceptional events occur.
![[Pasted image 20240211194258.png]]

(+) We somewhat found the problem for restricted privilege for programs, however, it is just for one process, if there are multiple process, `how can the OS know when and how can it switch between processes?`

###### Problem 2: Switching between processes
(+) If a process is running on the CPU, this mean that the CPU (which is the component that executes commands) is occupied by the said process, then how can the OS (which also needs the CPU) determine when and which to switch?

###### A. Cooperative Approach
(+) It turns out that programs relied heavily on `privilege actions` - which needs to be trap to the OS to perform. So one way or another, the program will `traps` back to the OS, effectively giving the CPU to the OS and the OS can determine if it needs to switch between process.
==> Thus, in a cooperative scheduling system, the OS regains control of the CPU by waiting for a system call or an illegal operation of some kind to take place. This however is a passive approach and is less than ideal if a process ends up in an infinite loop and never makes a system call.

###### B. Non-Cooperative Approach
(+) Without some additional help from the hardware, it turns out the `OS can’t do much at all when a process refuses to make system calls` (or mistakes) and thus return control to the OS. In fact, in the cooperative approach, your only recourse when a process gets stuck in an infinite loop is to resort to the age-old solution to all problems in computer systems: `reboot the machine`.
==> The answer turns out to be simple and was discovered by a number of people building computer systems many years ago: `a timer interrupt`. A timer device can be `programmed to raise an interrupt every so many milliseconds`; when the interrupt is `raised`, the currently running process is halted, and `a pre-configured interrupt handler in the OS runs`. At this point, the `OS has regained control of the CPU`, and thus can do what it pleases: stop the current process, and start a different one.

###### C. Saving and Restoring Context
(+) Now that the OS has regained control, whether through what approaches, it needs to make a decision, `to run the current process` or `to switch to a different one`. This decision is made by a part of the OS called the `scheduler`.
==> `If the decision is made to switch`, the OS then executes a low-level piece of code which we refer to as a context switch. A context switch is conceptually simple: all the OS has to do is save a few register values for the currently-executing process (onto its kernel stack, for example) and restore a few for the soon-to-be-executing process (from its kernel stack).
![[Pasted image 20240211200630.png]]

###### D. What about concurrency?
==> “Hmm... what happens when, during a system call, a timer interrupt occurs?” or “What happens when you’re handling one interrupt and another one happens? Doesn’t that get hard to handle in the kernel?”

==> One simple thing an OS might do is `disable interrupts` during interrupt processing; doing so ensures that `when one interrupt is being handled`, `no other one will be delivered to the CPU`. Of course, the OS has to be careful in doing so; disabling interrupts for too long could lead to lost interrupts, which is (in technical terms) bad. Operating systems also have developed a number of sophisticated locking schemes to protect concurrent access to internal data structures. This enables multiple activities to be on-going within the kernel at the same time, particularly useful on multiprocessors.