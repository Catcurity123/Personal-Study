#### A. Why do we need condition
(+) Locks allow one type of synchronization between threads - `mutual exclusion`
(+) Another common requirement in multi-threaded application - `waiting and signalling`
==> Thread T1 wants to continue only after T2 has finished some task.
=====> We need a new synchronization primitive: condition variables

#### B. What is a condition variable
(+) CV is a queue that a thread can put itself into when waiting on some condition
(+) Another thread that makes the condition true can signal the CV to wake up a waiting thread
(+) `Pthread` provides CV for user programs, OS has a similar functionality of wait/signal for kernel threads
(+) Signal wakes up one thread, signal broadcast wakes up all waiting thread
![[Pasted image 20240315092110.png]]

#### Problem 1: Why check condition in while loop:

![[Pasted image 20240315092755.png]]

(+) We need to do this in case the child has already run and done is true, then we dont need to wait
==> But why do we check the condition with `while` and not `if`.
(+) We do this to avoid corner cases of thread being woken up even when condition is not true

#### Problem 2: Why do we use lock when calling wait

![[Pasted image 20240315093027.png]]

(+) Parent checks done to be 0, decides to sleep, interrupted
(+) Child runs, sets done to 1, signals, but no one sleeping yet.
(+) Parent now resumes and goes to sleep forever.

==> Lock must be held when calling wait and signal with CV
==> The wait function releases the lock before putting thread to sleep, so lock is available for signaling thread

#### C. Example: Producer/Consumer problem
(+) A common pattern in multi-threaded programs
(+) Example: in a multi-threaded web server, one thread accepts requests from the network and puts them in a queue. Worker threads get requests from this queme and process them
![[Pasted image 20240315095032.png]]