#### Review the state of system

+ `ps`: Process Status, list the process status of each running process on the system
- `ps -u <username>`: Show process from 1 user
- `ps -e`: Show process from all user
- `ps -eH or ps -e --forest`: Show process from all user in hieracchi
- `ps -efH`: show full direct and the above

+ `top`: Interactive, near real-time monitoring of processes running on a system.
+ Inside `top terminal` press `k` key to prompt the `PID` to kill a process.

#### Monitor a process
+ `uptime`: View how long the system has been up, how many users, and CPU load average. The first column is load average for 1 minute, 5 minutes, and 15 minutes.
+ `free`: View used and available memory and swap space. `-m` for Megabyte, `-g` for Gigabyte.
+ `pgrep`: Find process information based on process name. `-a` to specify additional information.
+ `kill`: terminate processes. `kill -l`: to show available signal, `kill -<signal number>`: kill a process using a particular signal, `pkill -x <process name>`: to kill a process with exact match

#### Keep a process running
+ `watch`: runs a command at specified intervals. Used to monitor a command's output. `watch -n + <seconds>`: to specify interval.
+ `screen`: A terminal wndow manager that allows you tu run commands in an isolated session. `ctr + a + d`: to detach, `screen -r + <pid>`: to attach, `sreen -ls`: to list existing sreen.
+ `tmux`: a modern terminal window manager with extra feature, `ctr + b + d`: to detach, `tmux ls` to list screen, `tmux attach-session -t + <session number`: to retach.
+ `nohup`: Should a terminal window close, the process will still run as long as the login session is not terminated.
+ `jobs -l`: display a listing of jobs that are in the background.
		To take the job to foreground `fg + <job number>`
		To take the job back to background `bg %<jobnumber>`
		To kill the job running in background `kill + <PID>`





###### To kill all screen or tmux server
+ `screen -ls | grep Detached | cut -d . -f1 | awk '{print $1}' | xargs kill`:
+ `tmux kill-server`:
