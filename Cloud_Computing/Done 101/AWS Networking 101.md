#### A. VPC Sizing and Structure 
###### 1.  IP ranges to avoid
(+) `192.168.10.0/24` (192.168.10.0 -> 192.168.10.255)
(+) `10.0.0.0/16` (AWS) (10.0.0.0 -> 10.0.255.255)
(+) `172.31.0.0/16` (Azure) (172.31.0.0 -> 172.31.255.255.255)
(+) `192.168.15.0/24` (London) (192.168.15.0 -> 192.168.15.255) (Course)
(+) `192.168.20.0/24` (New York) (192.168.20.0 -> 192.168.20.255) (Course)
(+) `192.168.25.0/24` (Seattle) (192.168.25.0 -> 192.168.25.255) (Course)
(+) `10.128.0.0/9` (Google) (10.128.0.0 -> 10.255.255.255)

###### 2. IP Considerations
(+) VPC minimum `/28` (16 IPs), maximum `/16` (65456), and the first four and the last IPs is reserved by AWS.
(+) Personal preference for `10.x.y.z` range
(+) `Avoid common ranges` - avoid future issues.

==> When thinking about how many IPs should be allocated, think about how many regions AWS will operates in, after that add a buffer to protect from growth.

`NOTE`
(+) The 5 reserved IPs are as follows:
	(-) Network Addresss (10.16.0.0)
	(-) VPC Router (10.16.0.1)
	(-) DNS IPs (10.16.0.2)
	(-) Reserved for future use (10.16.0.3)
	(-) Broadcast Address (10.16.16.255)

###### 3. VPC Structure
(+) Minimum of 4 AZs (3 for minimum, one for buffer), and 4 tiers (Web, App, DB, and one buffer)
![[Pasted image 20240105102604.png | 600]]

###### 4. Custom VPCs
(+) Regional Service - All AZs in the region, Isolated network, nothing `in` or `out` without explicit configuration. The minimum private IPv4 CIDR Block is `/28` and maximum `/16`.
(+) Has the option to chose `default` or `dedicated tenancy`. If VPCs is in `dedicated tenancy`, all resources in the VPCs will be in `dedicated tenancy`.

###### 5. DNS in VPC
(+) Provided by R53, this is one of the reserved IPs in the 5 reserved IPs of VPC's subnet. The address is `Base IP + 2`.
(+) There is an option to `enableDnsHostnames` to give instances DNS Names and `enableDnsSupport` to enables DNS resolution in VPC.

###### 6. VPC Subnets
(+) `AZ resilient`, A subnetwork of a VPC - `within a particular AZ`.
(+) A subnet is in 1 AZ, 1 AZ may have one or more Subnet (or none).
(+) IPv4 CIDR is a subset of the VPC CIDR, and cannot overlap with other subnets.
(+) Subnets can communicate with other subnets in the VPC.
(+) DHCP Option Set can be created but can not be editted.

#### B. VPC Routing and Internet Gateway
###### 1. VPC Router
(+) Every VPC has a VPC Router - highly available, it is the second IPs that is reserved in any subnet. This device is used to route traffic between subnets.
(+) Controlled by `route tables` each subnet has one.
(+) VPC has a `Main route table`.
(+) Router is resilient for all AZs.

![[Pasted image 20240105111007.png]]

(+) One route table can be attach to multiple subnets but one subnet can only have one route table
(+) Local route always take precedence over other route

###### 2. Internet Gateway (IGW)
(+) Region resilient gateway attached to a VPC, as it operates in a region, we only need one IGW for one VPC.
(+) Meaning a VPC can have 0 or 1 IGW, and an IGW can be create and attach to 1 VPC or not attach at all.
(+) Runs from within AWS Public ZOne

![[Pasted image 20240105111601.png]]

###### 3. How IPv4 Addresses with an IGW
(+) When an IPv4 EC2 instance wants to fetch data from a Linux Update Server, it must have a Public IPv4, however, that is not how it works, an IPv4 EC2 Instance `never POSSESS any public address`, it only has the private IP.
(+) The Public address is created and managed in a record of IGW that maps the Private IP with the Public IP and the connection from and to external sources is done via the record in the IGW.
![[Pasted image 20240105112116.png]]


###### 4. Bastion Host / Jumpbox
(+) Bastion Host = Jumpbox
(+) An instance in a public subnet that accepts incoming management connections arrive. Then we can use the bastion host to access internal VPC resources.
==> This is usually is the only way to get access into the VPC in the past.

`Note`
(1) Creates VPC, assign a master IP range
(2) Creates subnets, attach subnet to the VPC, create IP range according to the architecture
(3) Create IGW, only 1 is needed for a VPC
(4) Create Route Table, attach the route table to the corresponding subnets, the IGW IP is `0.0.0.0/0` 
(5) Allocate IPv4 for the corresponding subnets.

![[Pasted image 20240105143603.png]]

#### C. Network Access Control Lits (NACLs)
(+) `NACL` can be though of as the `firewall` that surronds `Subnets` in a VPC, therefore, connection from resources in a subnet travelling to another subnet will go through NACL, but resources within a subnet when communicating will not go through NACL.
![[Pasted image 20240108113641.png]]

(+) By default, NACL is designed to allows everything both inbound and outbound.
(+) When a client making a connection to an instance with NACL applied, let's say client is making HTTPS request, meaning using `TCP/443`. From the instance's perspective, the inbound rule is TCP/443, but when the instance want to respond to the client, it will choose a random `ephemeral port` to transmit the data. Therefore, we need to implement a rule for this transmission.
![[Pasted image 20240108114344.png]]

(+) Things will get even more complicated if the web server is just an interface an it will need to connect with the application in another subnet, if that's the case, we will need to implement outbound and inbound rule for this connection in both the web's NACL and the app's NACL.
==> Therefore, to ease things up, NACL is by default allows everything on both ends.

`Note`
(+) When Bob is making a request, he is conducting the `Initiation` process, and when the web instance's connect with Bob it is conducting the `Response` process, these processes are 2 different streams of connection with 2 different rules. The instance makes no sense of the 2 streams and need to applied 2 different NACLs to them ==> Therefore, NACL is `stateless`, meaning it can not make sense of the connection stream.
(+) NACL only impacts data `crossing subnet border`, meaning it will only affects data coming from and to intances on different subnet.
(+) NACL can `explicitly allow and deny`.
(+) IPs/Networks, Port and Protocols are supported, not logical resources.
(+) NACLs cannot be assigned to AWS resources, only subnets.
(+) Use with SG's to add explicit Deny (Bad IPs/Nets)
(+) One subnet can only be assigned with one NACL at a time.
(+) Process in order from the lowest to the alterisk, ==> NACL is `order processing`.

#### D. Security Groups (SG)
(+) EC2 has one or more virtual network interface card (vNIC) attached to it, and data sent from and to EC2 will go through this vNIC, and `SG` is assigned to AWS resource, specifically the network interface.
(+) `SG` is `stateful`, meaning that `SG` remember the characteristic of the incoming connection, and when the outcoming connection matches it, it will automatically allows the outbound connection. This will mean that only one rule is needed for conenction, in the contrary to `NACL`.
(+) `SG` understands AWS's resources, therefore, not relying entirely on IPs or Networks. Meaning that we can refer AWS resources instead of its IPs.
(+) `SG` has an `hidden implicit deny` meaning that rules that are not `explicitly allows` will be understood as `denied` and 1 `SG` does not have `explicit deny`.

`Note`:
(+) `SG` is `Stateful`, meaning that SG sees `Initiation and Response` are the same thing.
(+) `SG` can filter based on `AWS Logical resources`
(+) `SG` has hidden implicit deny and explicit allows but dont have `explicit deny`
(+) `NACLs` is used on subnet for an products which dont work with SG's `(NAT Gateway)`
(+) NACLs is used when adding explicit Deny (Bad IPs, bad actors).
(+) SG is used as the default `almost everywhere`.

#### E. Network Address Translation (NAT) and NAT Gateway
(+) A set of different process used to remap SRC or DST IPs.
(+) `IP masquerading` - hiding CIDR Blocks behind one IP, mapping a lot of Private IP to one Public IP
(+) Gives Private CIDR range `outgoing` internet access.
(+) We can use NAT Instance (using EC2) or NAT Gateway.
![[Pasted image 20240108130303.png]]

`Note`
(+) If we need to give an Instance with an public IPv4 address access to the internet, we only need an `Internet Gateway (IGW)`.
(+) If we need to give an instance in private IPv4 zone, we will need a `NAT Gateway` to masquerading many IPs to one public IP, and an `Internet Gateway` to route the traffic.
(+) `NAT Gateway` needs to be run from a public subnet, and it uses `Elastic IPs (static IPv4 Public)`
(+) `AZ resilient service` (HA in that AZ), therefore, for region resilience we need to establish NATGW in each AZ, and Route Table in for each AZ with that NATGW as target.
(+) Managed service, scales to 45Gbps, bill according to the duration and data volume
(+) `NAT Gateway` does not support `Security Group`

###### 1. SSH Agent Forwarding
(+) We initially created a key pair when establishing the Bastion host, we keep the private key locally, and the host keeps the public key, when we make a connection to the Bastion host, we need to use the private key to prove to the bastion host that we are authorized entity.
(+) After that, we want to go to Private subnet, the host on private subnet do the same thing, it asks the Bastion host to prove itself, however, the bastion host does not have the private key, therefore, the access is denied.
(+) We can fix this by copying the private key onto the bastion host, but this is an anti-pattern because we dont want our private key to be everywhere and this is not scalable.
![[Pasted image 20240108134318.png]]

(+) The solution for this is using SSH Agent Forwarding.
![[Pasted image 20240108134809.png]]

(+) In this solution, we first add the private key as an `identity` into the `ssh-agent service`. We then connect to the Bastion host with the `-A` property to inform that whenever other hosts ask Bastion to identify its identity, the Bastion can forward that request to the `ssh-agent service`.
(+) When the Bastion host attempts to conenct to the Private Subnet, the PV ask BH to identify itself, BH then forward that request to the `ssh-agent service` and as the `ssh-agen service` has our `private key` added to its identity, the authentication process will be done and the access right is granted. Note that at no time does the private key leaves the client machine, the request is forwarded and verified at the client machine.
![[Pasted image 20240108140942.png]]

(+) Note that after adding our identity to the `ssh-agent service` we dont need to provide the private subnet with our private key anymore as the request is forwarded from the bastion to the ssh-agent and handled by the `ssh-agent service`.
(+) In the above example, although we successfully connect to the private instance from the bastion, the private instance still doesnot have conenction to the internet, as we did not create a NAT Gateway.

(+) To do this, we need to create a NAT gateway (for each AZ if we want a full VPC-resilient NATGW architecture) and assign it to the correct subnet. After that we will need to create a Route Table for the private subnet to be routed to the NATGW, and assign the route table to every subnets that needs connection to the NATGW.
![[Pasted image 20240108143046.png]]

==> The destination of the least specific IP for this route is `the NAT` meaning that whenever we perform ICMP to IPs that is not in the route table it will be redirected to the NAT, and the NAT will handle the rest.
![[Pasted image 20240108143515.png]]
==> The NATGW has a public IPv4 which will be used for connecting to the internet via the IGW.

![[Pasted image 20240108143256.png]]
==> The route table is associated with subnets that is private and needs connection to the internet (we can discard the db if we dont want db to connect to the internet).

![[Pasted image 20240108143624.png]]
==> With that we successfully implemented this architecture

`Note`:
(+) When we need to clean up the environment, as we delete the VPC, it said that Instances are running, Interfaces is running ,and NAT Gateway is running:
	(-) Instances can be terminated
	(-) Interfaces are for each instances and NATGWs that we allocated, so we needs to delete the NATGWs and terminated instances
	(-) NATGWs can be deleted, however, as NATGWs is attached with EIP, deleting the NATGWs does not mean that the EIPs will also be released, so we also needs to release the EIPs as EIPs is associated with an interfaces.
	==> Once we do that we can delete the VPCs.
