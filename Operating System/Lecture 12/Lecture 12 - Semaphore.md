#### A. What is a semaphore
(+) It is a synchronizaion primitive like condition variables
(+) Semaphore is a variable with an underlying counter.
(+) Two functions on a semaphroe variable.
	(-) `Up` or `Post` increments the counter
	(-) `Down` or `Wait` decrements the counter and blocks the calling thread if the resulting value is negative

#### B. Semaphore used as a single lock

(+) A semahore with init value 1 acts as a simple lock.
![[Pasted image 20240315100618.png]]

![[Pasted image 20240315101245.png]]

#### C. Semaphores for Ordering

![[Pasted image 20240315102749.png]]

![[Pasted image 20240315102916.png]]

#### D. Producer/Consumer
![[Pasted image 20240315103603.png]]

###### 1. What if lock is acquired before signaling
![[Pasted image 20240315104053.png]]