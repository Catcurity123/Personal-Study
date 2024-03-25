#### A. What is DNS
+ DNS is a `discovery service`
+ Translates machine into human and vice-versa => www.amazon.con => 104.98.34.131
+ It is huge and has to be distributed

![[Pasted image 20231226172120.png]]

==> `Zonefile` - Physical database containing `DNS Record` - A mapping of DM and IP, is hosted on a `Name Server`. Which can be query by the user using `DNS Resolver` to obtain the IP address.

`Terminology`:
+ `DNS Client`: Your laptop, phone, PC
+ `DNS Resolver`: Software on a server (ISP) which queries DNS on your behalf.
+ `Zone`: A part of DNS database (e.g. amazon.com)
+ `Zonefile`: physical database for a zone
+ `Nameserver`: where zonefiles are hosted

#### B. DNS Root
(+) DNS must have a starting point as it is important for knowing how DNS function. 
==> `www.amazon.com.`, the last `.` is the root, which is not shown in the browser

(+) On the top of the DNS tree lies the `DNS Root and Root Zone` which is just a database hosted by `13 DNS Root Servers`. Our Laptop will use the `DNS Resolver` provided by the `ISP` or in our local browser to search for the `Root hint` which is a `pointer` to the `13 Root Server`
![[Pasted image 20240202125339.png]]

(+) After using the `Root hint` to arrive at the `Root Zone` - which is a database containing `Top Level Domain (TLD)` and `Country Code TLD` managed by `IANA`. The `TLD` in `Root Zone` is managed by other companies or countries, `IANA` only `delegate` this to the user, meaning that the user trust `IANA`, `IANA` trust these `TLD provider` => user trust `TLD provider`, and the proces will continue until we get our IP for the DN we provided.

![[Pasted image 20240202130157.png]]

![[Pasted image 20240202130413.png]]

#### C. Route53 Product Basics
(+) `Register` Domains
(+) `Host` Zones, `Manage` nameservers
(+) `Global Service` with a single database => Global Resilience

###### 1. Register Domains
(+) To be able to do this `Route53` is delegated by companies that manage TLD, one of the companies is the `.org` domain hoster. 
(+) After user registered a domain, `Route53` create a `Zonefile - which contain DNS record of a domain` and hosts it on `4 separate NameServer`, after that `Route53` creates a `NameServer Record - a pointer to the NameServers` at `.org zone`. Therefore, whenever a user types `animals4life.org.` 

![[Pasted image 20240202132506.png]]


=> The PC will use `DNS Resolver in the browser or the ISP` to obtain `Root hint`, `Root hint` is a pointer to the `13 nameservers hosting Root Zone, also called DNS Root Server`, at the `DNS Root`, the process will then ask for `www.animals4life.com.` but the `DNS Root zone does not have` but it will guide the process to a its delegation which is `.org`. The process will then go to `.org` and ask for `www.animals4life.com` which the `.org zone does not have`, but it will guide the process to its delagation, which is `animals4life.org`, then when the process arrives at `animals4life.org zone` it will ask for `www.animals4life.org` and obtain the IP address of the designated DN.

###### 2. Hostedd Zones - DNS as a service
(+) Zone files in AWS are hosted on four managed nameservers. It can be either public or private (Linked to VPCs and only accessible via these VPCs).


###### 3. DNS record types
(+) `NameServer Record (NS)`: which is the record that contains the pointer to `end-point` DNS Server, which contains the IP for our wanted DN.
![[Pasted image 20240202133041.png]]

(+) `A and AAAA Records`: these are the records that map `hostname` to `IP`, A record maps a host to an `IPv4` address, AAAA record maps a host to an `IPv6` address.
![[Pasted image 20240202133319.png]]

(+) `CNAME Records - Canonical name Records`: these are the records that map `host` to `host`, let's say we have an `A record that points to an IPv4 address`, if that server has multiple function like `FTP, mail and WWW`, we can create 3 `CNAME records` and point it to an `A record`, so that they will have the same IPv4 address and whenever that IPv4 gots updated the rest of the functions. ==> `CNAME` can not be pointed directly to an IPv4 address.
![[Pasted image 20240202134006.png]]

(+) `MX Records - Mail Exchange Records`: This is crucial to know how email works, this record identifies the email server responsible for accepting incoming email messages for a specific domain. It specifies the domain's mail server or servers that should be used for handling email delivery.

```
Domain: example.com
MX Record: example.com MX 10 mail.example.com
```

In this example, the MX record specifies that the mail server responsible for handling incoming email for `example.com `is `mail.example.com` with a priority of `10`. When an email is sent to an address ending with `@example.com`,  the sender's email server will query the DNS system, find the MX record, and route the email to the "`mail.example.com`" server for further processing and delivery to the recipient's mailbox.
![[Pasted image 20240202135016.png]]

(+) `TXT Records`: add arbitrary text information with a domain or subdomain

```
Domain: example.com  
TXT Record: example.com TXT "v=spf1 include:_spf.example.net -all"`
```

In this example, the TXT record is used to specify the SPF policy for the domain "example.com". The text data "v=spf1 include:_spf.example.net -all" indicates that emails sent from servers listed in the "_spf.example.net" domain are authorized to send emails on behalf of "example.com", and all other servers should be rejected.
![[Pasted image 20240202135738.png]]

(+) `Time To Live`: When a DNS resolver receives a DNS response containing a record, it stores the record in its cache. The TTL value associated with the record determines how long the resolver should consider the record valid before querying the authoritative DNS server again for updated information.
![[Pasted image 20240202135729.png]]

(+) `Alias`: map a Name to an `AWS resource`. can be used both for `naked/apex` and `normal` records. 
(+) For `non apex/naked` - functions like a `CNAME`.
(+) There is no charge for alias request pointing at `AWS resource`.


