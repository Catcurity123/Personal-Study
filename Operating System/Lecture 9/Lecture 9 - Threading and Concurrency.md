#### A. Single threaded process
(+) So far, we have studied single threaded programs
(+) Process execution including:
	(-) `Program Counter` points to current instruction being run.
	(-) `Stack Counter` points to stack frame of current function call.
==> A program can also have multiple threads of execution
=====> So what is a `thread`?

#### B. Multi threaded process
(+) A thread is like another copy of a process that executes independently
(+) Threads shares the same address space (code, heap)
(+) Each thread has separate PC ==> Each thread may run over different part of the program.
(+) Each thread has separate stack for independent `function calls`
![[Pasted image 20240219155117.png]]

###### What is the difference between process and threads
(+) Parent P forks a child C
	(-) P and C do not share any memory
	(-) Need complicated IPC mechanisms to communicate
	(-) Extra copies of  code, data in memory

(+) Parent P executes two threads T1 and T2
	(-) T1 and T2 share parts of the address space
	(-) Global variables can be used for communication
	(-) Smaller memory footprint

(+) Threads are like separate processes, except they share the same address space.

![[Pasted image 20240219155410.png]]
#### C. Why do we need threads?
(+) `Parallelism:` a single process can effectively utilize multiple CPU cores.
	(-) Understand the difference between concurrency and parallelism

==> `Concurrency`: running multiple threads/processes at the same time, even on single CPU core, by interleaving their executions.

==> `Parallelism`: running multiple threads/processes in parallel over different CPU cores.

(+) Even if `no parallelism`, concurrency of threads ensures effective use of CPU when one of the threads `blocks` (e.g., for I/O)

###### Example: Creating threads using pthreads AI
![[Pasted image 20240219160319.png]]


###### Example: Threads with shared data
![[Pasted image 20240219160835.png]]

###### But sometime, the result is a lower value 
![[Pasted image 20240219160905.png]]

###### This is not a bug but an expected behaviour of multi threaded program
![[Pasted image 20240219160950.png]]

(+) This is called `a race condition`, whereas, concurrent execution can lead to different results.
(+) Portion of code that can lead to `race condition` is called `critical section`, and in the essence of `critical section` we need `mutual exclusion` meaning only one thread should be executing `critica section` at any time
==> We need an `atomicity` of the `critical section`, meaning the critical section should be executed like one uninterruptible instruction
=======> This is achieved using `Locks`.