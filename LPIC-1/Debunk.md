#### 1.Normal file and Pseudo File

+ `Regular file `refers to a file **that contains data**. It is the most common type of file found in a file system. It can stored text, binary data, code, and configuration file.
+ `Pseudo file` refers to a special files in Linux file system that **do not contain traditional data** but instead provide a **mechanism to access system information or interact with system component**.

#### 2. Firmware, Kernel, Module, Device driver, Application

+ `Firmware` refers to **software that is embedded in hardware devices**. It **provides low-level control and functionality** specific to the hardware it resides in. `BIOS` and `EUFI` are **firmware stored in ROM or flash memory in motherboard**. 
+ `Kernel and Module` are the **component that makes up of Linux OS**. `Kernel` is a piece of software that **handle resource management** for hardware such as memory management, CPU scheduling, and device communication. `Module` are loadable pieces of code that can be added or removed from the running kernel to **extend the functionality of a system**.
+ `Device driver` is a a **specific type of module** or software component that **enables communication and interation bettwen OS and hardware device**. It serves as an interface between the higher-level software layer and hardware.
+ `Application is software` program that runs in the User Space of an OS. It relies on device drivers and other system software to access and utilize hardware devices.

#### 3. Boot Sequence

1. Power on the computer.
2. Power reaches the `motherboard` and start the `non violatile memory` which is the `ROM - Read Only Memory` component, in `ROM` there are firmwares that **initialize hardware, perform system checks, detect boot devices and hand off control to boot loader.** The computer either uses BIOS or EUFI as firmware to boot the system.
3. The firmware read information in the boot device (either the USB or hard drive) found in the first sector of a storage device to find the boot loader.
4. The bootloader then start (usually GRUB - Grand Unified Bootloader) to load the **kernel** onto **RAM**, **Kernel** then load essential **Kernel modules**, which is but not only **Device Driver**, onto **RAM**.
5. After finished loading, the Kernel hand over the control of the machine to `init` in `/sbin/init`.
6. The `init` process then seek information and action in `/etc/inittab` such as runlevel and start up scripts.
7. After executing all the scripts, the system is now ready to use.

#### 4. Daemon, Service, and Unit File

+ `Daemon` is **background process that runs independently of user interaction**. It is typically associated with Unix-like OS. **Daemons perform specific tasks or provide services**, such as managing network services, handling print queues, or monitoring system health.
+ `Service` is generally refers to a **software component or process that provides specific functionality** or access to resources.
+ `Unit File` is a c**onfiguration file used by `systemd`**, which is responsible for managing services, daemons, and other system components. A `Unit File` **provide information about a specific system component.**

>[!Note] What is the relationship between `unit file` and `target`?
>`Target` defines a desired state for the system, `unit file` consists of configuration details about service, device, mount point that needed for the state to be achieved.
>Therefore, a `Target` consists of multiple `unit file`. For example, `multi-user.target` consists of `networking.service` and multiple other `.service` unit file.


#### 5. Partition and Mount

+ `Partition` is the act of **creating some space out of a storage drive**. For example a storage device is connected to the system, it will appear on` /dev` as `/dev/sda`, we partition it to /`dev/sda1` so that it is usable, then we further partition it to `/dev/sda2`, `/dev/sda3` to create multiple logical space for our usage.
+ `Mount` is the act of **assigning partitions to chosen directories** so that there is separation between directoies. For example we will mount `/dev/sda1` to the root directory `/`, `/dev/sda2` to `/home`, `/dev/sda3` to `/boot`.
