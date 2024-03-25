(+) Process priority ranges from:
	(-) -20: highest priority
	(-) 19: lowest priority
	(-) 0: default for mopst processes
(+) Only root or privileged account can lower the priority of a process.

#### How to change priority level
+ `ps -o pid,nice,cmd,user`: Show process
+ `nice -n <priority number> <command>` => `nice -n 4 watch -n 3 date &`
+ `renice -n <priority number> <PID>`=> `renice -n -1 2358`

#### How to change priority level in top
+ Enter top using `top
+ Press `U` to enter username
+ Press `R` to `renice process` follows by the priority number.

(+) We can increase the priority level (making it less favorable) but we cannot decrease it (making it more favorable) without appropriate permission.