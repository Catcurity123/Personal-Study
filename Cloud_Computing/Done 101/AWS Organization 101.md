#### A. Service Control Policies
(+) SCP is a JSON document that is applied to the whole `organization` or `individual Organization unit` or applied to `individual AWS Account`.
(+) `Management Account` is not affected by `SCP`, therefore, it is not normally used for any AWS Resource.
(+) SCPs are `account permissions boundaries` meaning they limit what the account (including account root user) can do.
(+) SCPs don't grant any permission, they are there to act as a bondary for what an account can and cant do. SCPs has `FullAWSAccess` right by default. 

###### A.1 Allow list and Deny list
(+) `Allows list` denies everything by default and only allows required permissions
(+) `Deny list` allows everything by default and only denies required permissions.

==> The permission right of an entity with SCP is the overlap between rights of the account itself and rights present in SCP
![[Pasted image 20240103114956.png | 500]]




