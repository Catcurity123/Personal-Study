#### A. Variable sized allocation
(+) Given a block of memory, how do we allocate it to satisfy various memory allocation request?
==> This problem must be solved in the `C lib`, allocate one or more pages from kernel via `brk/sbrk` or `mmap` system calls

#### 1. Headers
(+) Consider a simple implementation of `malloc` 

``` C
int main() {
    int* array;
    int size = 5;
    
    // Dynamically allocate memory for an array of integers
    array = (int*) malloc(size * sizeof(int));
```

![[Pasted image 20240219152100.png]]

(+) Every allocated chunk has a `hedaer` with info like size of chunk.
==> This is done to know how much memory to free when `free()` is called

```C
free(array);
```

![[Pasted image 20240219152201.png]]

#### 2. Free list
(+) Free space is managed as a list
	(-) Pointer to the next free chunk is embedded within the free chunk
(+) THe library tracks the head of the list
	(-) Allocations happen from the head.

![[Pasted image 20240219152448.png]]

#### 3. External fragmentation
![[Pasted image 20240219153041.png]]

#### 4. Splitting and Coalescing
![[Pasted image 20240219153241.png]]

#### 5. Buddy allocation for easy coaslescing
![[Pasted image 20240219153402.png]]

==> Always allocate memory `the closest power of 2 to the request`.

