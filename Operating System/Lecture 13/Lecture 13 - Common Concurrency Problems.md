#### A. Non Deadlocks bug

###### 1. Atomicity bugs
(+) Atomicity assumptions made by programmer are violated during execution of concurrent threads
==> `Fix`: Locks for mutual exclusion

![[Pasted image 20240315104927.png]]

==> In the above example, after `thread 1` check if `proc_info` is not `NULL` and attempt to put it, it is interrupted and `thread 2` change `proc_info` to NULL `making` printing of thread 1 `invalid` according to our logic.

(+) To fix this, always use locks when accessing shared data

![[Pasted image 20240315105244.png]]

###### 2. Order-violation bugs
(+) Order-violation bugs - desired order of memory accesses is filpped during concurrent execuion
==> `Fix`: Condition variables.

![[Pasted image 20240315105335.png]]

==> In the above example, `Thread 1` init a variable, but while doing that, it is interrupted to `Thread 2`, where the variable need to be used, but it has not been initialized. This is an example of order-violation bugs.

(+) To fix this, we can use condition variables or semaphores
![[Pasted image 20240315105922.png]]

#### B. Deadlock bugs
(+) `Classic example`: Thread1 holds lock L1 and is waiting for lock L2. Thread2 holds L2 and is waiting for L1
![[Pasted image 20240315110309.png]]

###### Deadlock only occurs if all of the following happen:
(+) Mutal exclusion: a thread claims exclusive control of a resource (e.g., lock)
(+) Hold-and-wait: thread holds a resource and is waiting for another.
(+) No preemption: thread cannot be made to give up its resource (cannot take back a lock)
(+) Circular wait: there exists a cycle in the resource dependency graph.
==> All for of the above conditions must hold for a deadlock to occur.

###### How to prevent circular wait
(+) Acquire locks in a certain fixed order => Both threads acquire L1 before L2.
(+) Total ordering (or even a partial ordering on related locks) must be followed ==> Order locks by address of lock variable
![[Pasted image 20240315111838.png]]

###### How to prevent hold-and-wait
(+) Acquire all locks at once, say, by acquiring a master lock first
(+) But this method may reduce concurrent execution and performance gains
![[Pasted image 20240315112052.png]]