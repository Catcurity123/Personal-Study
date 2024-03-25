#### Non-Journaling:
(+) `ext2`: legacy Linux file system, releases in 1993


#### Journaling:
(+) Uses a journal to keep track of changes that have not yet been written to the file system.

(+) `ext3`: released in 2001, introduced journaling to ext2

(+) `ext4`: released in 2006, added extra features, meant to be a `stop-gap` util a better solution comes along

(+)`SFX`: created in 1993, for IRIX OS. Ported to Linux in 2001, currently the default for Red Hat Enterprise 7 and CentOS 7

`btrfs`: 
(+) Uses CoW (Copy on Write) and uses subvolumes, similar to a partition and can be accessed like a directory.
![[Pasted image 20240306144132.png]]

(+) Can also provide snapshot, that reference the original data's location, includes metadata and directory structure.

`FAT File System`: File Allocation Table
(+) Linux can use VFAT (Virtual File Allocation TAble) which allows for long file names.
(+) EFI boot partitions need to use a FAT Partition (on Linux this will be a vfat paritition).

`exFAT` - Extended FAT FIle System:
(+) Allows for file larger than 2GB in size.
(+) Primarily used for external disk drives, thumb drives.