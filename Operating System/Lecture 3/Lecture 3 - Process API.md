#### A. What API does the OS provide to user programs?
(+) API stands for Application Programming Interface, which is just a fancy way of addressing the `functions available` to write user programs
(+) API provided by OS is a set of `system calls`.
	(-) System call is a function call into OS code that runs at a higher privilege level of the CPU
	(-) Sensitive operations (e.g., access to hardware) are allowed only at a higher privilege level.
	(-) Some `blocking` system calls cause the process to be blocked and descheduled (e.g., `read from disk`).

(+) `POSIX API`: a standard set of system calls that an OS must implement
	(-) Program written to the `POSIX API` can run on any `POSIX` compliant OS
	(-) Most modern OSes are `POSIX` compliant
	(-) Ensures program portability

(+) Program language libraries hide the details of invoking system calls
	(-) The `printf` function in the C library calls the `write` system call to write to screen
	(-) User programs usually do not need to worry about invoking system calls.

#### B. Process related system calls (in Unix)
(+) `fork()`: creates a new child process
	(-) All processes are created by forking from a parent
	(-) The init process is the ancestor of all processes.
(+) `exec()`: makes a process execute a given executable
(+) `exit()`: terminates a process
(+) `wait()`: causes a parent to block until child terminates.

###### 1. What happens during a fork?
(+) A new process is created by making a copy of parent's memory image.
(+) The new process is added to the OS process list and scheduled
(+) Parent and child start execution just after fork(with different return values)
(+) Parent and child execute and modify the memory data independently after creation.
![[Pasted image 20240210230856.png | 600]]

###### 2. The relationship between child and parent processes
(+) Process termination scenarios
	(-) By calling exit() (exit is called automatically when end of main is reached)
	(-) OS terminates a misbehaving process.
(+) Terminated process exists as a `zombie` - meaning it is not cleared
(+) When a parent calls `wait()`, zombie child is cleaned up or `reaped`
(+) `wait()` blocks in parent until child terminates (non-blocking ways to invoke wait exist)
![[Pasted image 20240210232015.png | 600]]

#### C. What happens during exec
(+) After fork, parent and child are running same code => `not too useful`
(+) A process can run `exec()` to load another executable to its memory image => so, a child can run a different program from parent.
(+) Varians of `exec()`, e.g., to pass comandline arguments to new executable.
![[Pasted image 20240210232556.png | 500]]
#### E. Case Study: How does a shell work?
(+) In a basic OS, the `init` process is created after initialization of hardware.
(+) The `init` process spawns a shell like `bash`.
(+) Shell reads user command, `fork` a child, `exec` the command executable, waits for it to finish, and reads the next command provided by the user.
(+) Common commands like `ls` are all executables that are simple `exec'ed` by the shell.
![[Pasted image 20240210232915.png]]

(+) Shell can manipulate the child in strange way, suppose we want to redirect output from a command to a file `prompt> ls > foo.txt`
(+) Shell spawns a child, rewires its standard output to a file, then calls `exec` on the child

![[Pasted image 20240210233202.png]]
