#### A. What is virtualization
(+) Virtualization refers to the process of running more than 1 OS on a physical hardware. Before virtualiztion, a system's architecture is like this:
![[Pasted image 20240108150036.png]]
(+) We have the` physical hardware` being controlled by the `OS`, which is running in the `priviledged mode` (specifically a small part of the OS - the `Kernel`), there are `Applications` or `software` in the machine, which is running in the `user mode`, when the Applications want to utilize the hardware, it needs to perform a `system call` to the `kernel` for permission to do so. Attempts to use the hardware without doing so will result in an error.
(+) Virtualization allows for multiple OS to run on a same device. Initially, Virtualization is extremely inefficient as alll the `priviledged calls` to the hardware are done in softwares and the hardwares is not awared of it.
![[Pasted image 20240108151414.png]]

###### 1. Emulated Virtualization.
(+) With this method, the host's OS is still runned on the hardware and it is included with an additional capabilities known as athe hypervisor, it is runned in the priviledged mode with full access to the hardware. The other OS run on the machine is known as the Guest OS is wrapped in a container known as the VMs.
(+) The VMs believed that it has hardware attached to it like the CPU, Memory, or Network. However, these are all emulated, fake information provided to the VMs. The point to remember about emulated virtualization is that the VMs still believe they have their own hardware, so they still perform priviledged calls to the hardware, trying to take controls of the hardware they believe were theirs, despite the fact that these are just emulated information allocated to them on the host's hardware by the hypervisor. Without the arrangement of the hypervisor, the system would crash or the memory will be override by the guest's OS.
(+) The arrangement done by the hypervisor using `Binary Translation` as it translated priviledged calls from the guest's OS on the fly to the Host's hardware. The key part is that this translation process is done in the software and is very slow. therefore this virtualization is never made to the market for this performance drawback.
![[Pasted image 20240108153337.png]]


###### 2. Para-Virtualization
(+) This only works on a small subset of Guest's OS, as they are modified Linux OS that makes user calls to the Hypervisor called Hypercalls. Then the Hypervisor will makes priviledged calls to the hardware.
(+) This method make the hardware almost `virtualization-awared` and massively increase performance, however, it is still done via software that aims to trick the guest's OS.
![[Pasted image 20240108153622.png]]

###### 3. Hardware Assisted Virtualization
(+) With this method the hardware is `virtaulization-awared`
![[Pasted image 20240108154112.png]]


