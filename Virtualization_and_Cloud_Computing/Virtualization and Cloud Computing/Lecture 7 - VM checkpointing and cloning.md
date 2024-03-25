#### A. VM checkpointing for high availability
(+) Checkpoint VM memory periodically at backup VM to recover from failures.
(+) If primary VM fails, backup VM takes over execution using checkpoint state.

#### B. VM cloning for parallel execution of tasks
(+) Fork a VM with identical state to parent VM to execute tasks in parallel.
(+) Inspired by idea of forking processes.

#### C. Techniques for reliability
(+) `Application-based replication`:
	(-) Application communicates with other replicas and replicates state
	(-) Consensus protocols like Raft or Paxos used to maintain consistency of replicated state.
	(-) Application decides what state to replciate
	(-) Reliability via changes to application code (not for legacy apps).

(+) `VM-based replication`, or whole system replication
	(-) Entire VM state (memory, CPU, disk of apps, kernel) is replicated
	(-) Higher overhead than application-based replication
	(-) Does not require application code changes
	(-) VM provides easy way to capture whole system state.
![[Pasted image 20240219163512.png]]

###### Remus VM replication for high availability
(+) Primary-baackup system, can tolerate single host failures
	(-) primary VM runs the application
	(-) Backup VM gets checkpoint of primary VM state periodicaly
	(-) If primary fails, backup resumes from lastest checkpoint.
(+) Periodically (few tens of milisec), primary captures all its state (dirty pages, CPU state, etc., and transmits it to back up => similarly to iterative pre-copy
(+) Once backup VM stores this checkpoint, it sends ack back to primary
(+) While waiting for ack from backup VM:
	(-) Network output buffered, client will see responses only after checkpoint is done
	(-) But primary continues execution speculatively, to avoid slowdown
==> This is called `Asynchronous replication with speculative execution`.
