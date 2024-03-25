#### How to manage disk usage
(+) `df`: Disk Free, display storage usage of the disk. `-h` to display in Gigabyte, `df -h <storage name>` for a particular storage.
	(-) `df --total -h`: display total usage of disk 

(+) `du`: Disk Usage of the current directory, `-h` to display more nicely.
	(-) `du -sh`: summarize information in MB.
	(-) `du -h -max--depth=2 > <location>`: see only 2 directories

(+) `inode`: an inode (index node) stores information about files and folders such as: permissions, ownership, and file type. Most file systems contain a maximum number of inodes that it can contain, the following command display inode information
	`ls -i`
	`df -i`
	`du --inodes`


#### How to manage a file system
