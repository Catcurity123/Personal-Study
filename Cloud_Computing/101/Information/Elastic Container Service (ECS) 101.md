#### A. Elastic Container Service
(+) `Container Definition` - includes Image & Ports.
(+) `Task Definition` - Security (Task Role), Container(s), Resources
(+) `Task Role` - IAM Role which the Task assumes
(+) `Service` - How many copies, HA, Restarts

#### B. ECS Cluster Types

###### 1. ECS - EC2 Cluster Mode
![[Pasted image 20240129165137.png]]

###### 2. ECS - Fargate Mode
![[Pasted image 20240129165548.png]]

###### Summary. EC2 vs ECS(EC2) vs Fargate
(+) If you use containers => `ECS`
(+) `Large` workload - `price` conscious - `EC2 Mode`
(+) `Large` workload - `overhead` conscious `Fargate`
(+) `Small/Burst` workload - `Fargate` as we pay for what we consume, EC2 makes us pay even when we dont.

#### C. Bootstrapping EC2 using User Data
(+) Bootstrapping allows `EC2 Build Automation`.
(+) User Data -Accessed via the meta-data IP
(+) `http://169.254.169.254/latest/user-data`.
(+) Anything in User Data is executed by the instance OS
(+) `Only on Launch`
(+) EC2 doesnt interpret, the OS needs to understand the User Data

###### 1. EC2 Bootstrapping
![[Pasted image 20240130141808.png]]
(+) It's opaque to EC2, its just a `block of data`.
(+) It's not secure - don't use it for passwords or long term credentials.
(+) User data is limited to 16KB in size
(+) Can be modified when instance stopped.
(+) Onlu executed once at launch



