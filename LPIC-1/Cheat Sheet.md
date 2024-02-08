#### 1.1 Pseudo File System 

+ `cd /proc`: for process running on the machine. 
+ `cd /sys`: for process about the machine.

#### 1.2 Investigating Hardware

+ `cd /dev`: for device on the machine.
+ `lspci`: List Peripheral Component Interconnect, list all the peripheral components. 
+ `lsusb`: List Universal Serial Bus, list all USB devices.
+ `lsblk`: List Block Devices, list information about block device on a system.
+ `lscpu`: List Central Processing Unit, list information about processor on a system.

#### 1.3 Working with Kernel Modules

+ `uname`: Unix Name, this is used to display information about the currently running kernel.
+ `lsmod`: List Modules, list information about currently loaded modules.
+ `modinfo`: Module Information, list details about a module.
+ `modprobe`: Module Probe, add module or add `-r` to remove a module.

#### 1.4 Linux Boot Sequence

+ `/sbin/init`: to view init system.
+ `/etc/inittab`: to see the subsequent process that will be run by init.
+ `/usr/lib/systemd/system`: to view user-specific units
+ `/etc/systemd/system`: to view system-level units
+ `systemctl list-unit=file`: View all unit file on a system.
+ `ps`: Process Status, this command will retrieve details such as process that are currently running on the machine
`=>` `ps -p 1`: will return the first process being run on the machine.

#### 1.5 Change Runlevels, boot targets and Shutdown or Reboot the System

+ `runlevel`: View current runlevel
+ `telinit + level`: change to another runlevel, but on some system you can only move from 3 to 5 but not 5 back to 3
+ `systemctl cat + target`: view the content of a target
+ `systemctl list-unit-file -t + target`: view all the system target
+ `systemctl list-units -t + target`: view all the target that are currently active
+ `systemctl get-default`: get the default target
+ `systemctl set-default + target`: set default target
+ `systemctl isolate + wanted target`: change to another target
+ `wall`: broadcast a message to all logged in user.
+ `telinit 6`, `reboot`, `shutdown -r now`, `systemctl isoalte reboot.target`: reboot the system.
+ `poweroff`, `telinit 0`, `systemctl isolate poweroff.target`: poweroff the system
+ `shutdown -h 1 minute`: this will tell the machine to halt for one minute before shutting down.
+ `shutdown -c`: cancel the shutdown process.
+ `systemctl status display.manager.service`: this will display the service responsible for UI. Use this command to check if the system support graphical interface

#### 2.1 Design Hard Disk layout

+ `/`: bottom of the directory tree, the `root`.
+ `/var`: variable location, where `log file`, and `dynamic content` such as websites are often found here.
+ `/home`: the `user's home directory`, where personal files are stored.
+ `/boot`: the boot directory, where `linux kernel and supporting files` are stored
+ `/opt`: `optional` software often used by third-party vendors.
+ `mount`: can be used to mount partitions to directories, or show all existing mounts.
+ `lsblk`: short for List Block, used to show all block devices on a system.
+ `fdisk -l /dev/diskname`: short for Fixed Disk, can be used to list out partition information on a specific disk.
+ `swapon --summary`: show summary of the swap usage on a system.

#### 2.2 Introduction to LVM 

+ `pvs`: short for Physical Volume Scan, will list all physical volumes in an LVM-group.
+ `vgs`: short for Volume Group Scan, will list all volume group scan in an LVM-group.
+ `lvs`: short for Logical Volume Scan, will list all logical volumes in an LVM-group.

#### 2.3 Install a Boot Manager

#### 2.4 Interacting with the Boot Loader

#### 3.1 Manage Shared Library

+ `/lib`, `/usr/lib`, `/usr/lib64`, `/usr/local/lib`, `/usr/share`: are the location that library can be found
+ `ldd`: List Dynamic Dependencies, which is used to display the shared object dependencies of an executable or shared library files.
+ `ldconfig`: Linker/Loader Configuration, which is used to update the cache of shared library locations and symbolic links
+ `/etc/ld.so.conf`: configuration file used by the dynamic linker (ld.so) to determine the directories for shared libraries.

#### 3.2 Advance Package Tool (APT)
+ `/etc/apt/sources.list`: Configuration file that lists out repository location for packages.
+ `apt-get update`: update the local apt cache with a listing of packages that can be updated /upgraded and installed.
+ `apt-get upgrade`: upgrades the packages that have upgrades available.
+ `apt-get install`: install a package from the repositories in the source list file.
+ `apt-get remove`: remove a package but will leave behind configuration file
+ `apt-get purge`: remove the package and configuration file
+ `apt-get dist-upgrade`: upgrade all packages on the system to the next release of the distribution.
+ `apt-get download`: download the package but does not install it.
+ `apt-cache search <package_name>`: search for packages matching the specified string.
+ `apt-cache show <package_name>`: show detailed information about a specific package.
+ `apt-cache depends <package_name>`: display dependencies of a specific package.
+ `apt-cache policy <package_name>`: display installation status and version of a specific package.

#### 3.3 The Debian Package Utility

+ `dpkg --info`: display information on a package.
+ `dpkg --status`: less details version of `dpkg --info`.
+ `dpkg -l`: list out packages that match the string provided.
+ `dpkg -i`: installed specified package.
+ `dpkg -L`: lists out all files that were installed with a specified package.
+ `dpkg -r`: remove a specified package but leave behind configuration file.
+ `dpkg -P`: removes a specified package and also any configuration file.
+ `dpkg -s`: searches through the package database for a file specified and list out any matching.
+ `dpkg-reconfigure`: allows for the modification for a package.

#### 3.4 The Yellowdog Updater, modified

- Global YUM configuration options are set in `/etc/yum.conf`
- Reads repository information from `/etc/yum/repos.d`
- Caches latest repository information in `/var/cache/yum`
- `yum update`: this command will read each repository within `yum.repos.d` and search for any updated packages available for the system.
- `yum search`: search the yum repositories for a specified package
- `yum info`: Lists information about a specified package.
- `yum list installed`: display all installed packages.
- `yum clean all`: clean up all of yum's cache information and its local database file
- `yum install`: install a specified package and all of its dependencies
- `yum remove`: uninstall a package, leaves dependencies behind.
- `yum autoremove`:uninstall a package and its dependencies.
- `yum what provides`: find out what package provides a specified file name.
- `yum reinstall`: reinstall a specified package