+ A VPIC is within `1 account` and `1 region`, so it is `regionally resilient`.
+ There are 2 types of VPC, `1 default VPC` and `many Custom VPCs`.

![[Pasted image 20231103175148.png]]

#### Default VPC 
+ One per region
+ Default VPC CIDR is always 172.31.0.0/16
+ /20 subnet in each AZ in the region
+ It is configured with Internet Gateway (IGW), Security Group (SG), and NACL


