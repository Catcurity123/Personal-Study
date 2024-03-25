#### C. Storage Refresher
###### 1. Storage Terms
(+) `Direct (local) attached Storage` - Storage on the EC2 Host, it is fast but if the disk, or hardware fails, the disk is lost and if the EC2 instance moves to a different host, the disk is lost. As this storage option is directly attached to the EC2 instance.
(+) `Network Attached Storage` - Volumes delivered over the network (EBS), which is highly resilient and is separated from the EC2 instance (but in the same AZ).
(+) `Ephemeral Storage` - Temporary storage
(+) `Persistent Storage` - Permanently storage - lives on past the lifetime of the instance.

(+) `Block Storage` - volume presented to the `OS` as a collection of blocks, no structure provvided. `Mountable` and `Bootable`. The thing about `Block Storage` is that is has no structure provided, just a collection of uniquely identified blocks, the addressing and structure of data is done by the OS via filesystem.
(+) `File Storage` - Presented as a file share has structure, `mountable` `not bootable`.
(+) `Object Storage` - collection of objects, flat. `Not mountable` and `Not bootable`.

###### 2. Storage Performance
![[Pasted image 20240109125646.png]]

(+) `IO or block size`: the block size specifies the amount of data transferred at once between the volume and the instance attached to it. EBS supports 4 different block sizes: 1KiB, 4KiB, 8KiB, and 32 KiB.
(+) `Input/Output Operation Per Second (IOPS)`: refers to the number of read/wrtie requests a particular storage device can handle concurrently.
(+) `Throughput`: While `IOPS` determine how quickly individual operations occur, throughput describes the total rate at which data moves across the storage interface.

#### D. Elastic Block Store (EBS)
(+) Provide `Block Storage` - `raw disk allocations (volume)` - can be encrypted using KMS
(+) Instance see `block device` and create `file system` on this device (ext3/4, xfs)
(+) Storage is provision in `one AZ` (Resilient in that `AZ`)
(+) Attached to one EC2 instance (or other service) over a `storage network`.
(+) EBS Volume can be `detached` and `reattached`, it is not lifecycle-linked to one instance, it is `persistent on its own` until we delete it.
(+) `Snapshot` (backup) into `S3`. Create volume from snapshot (migrate between AZs).
(+) Different physical storage types, different sizes, different performance profiles.
(+) Billed based on `GB-month` (and in some cases `performance`)
(+) EBS replicates within an AZ. Failure of an AZ means failure of a volume. The solution for this is we can create a `Volume Snapsots` in S3. As S3 is a global service with regional resilient, we can copied that snapshots accross AZs to provide more resilient, and we can copied that snapshots to another region for more resilient.
![[Pasted image 20240109132524.png]]

###### 1. EBS Volume Types and General Purpose SSD
(+) `GP2` can be as small as 1GB or as large as 16TB.
(+) `IO Credit` is 16KB chunk of data, so if we transfer 160KB of data, it is 10 IO Credit, and 1 IOPS is 1 IO in 1 second
(+) `IO Credit Bucket` has a capacity of 5.4 million IO credit, fills at rate of `Baseline Performance`. The bucket is fills with minimum `100 IO Credit` per second `regardless of volume size`. Beyond 100 minimum, the bucket fills with `3 IO Credit per second, per GB of volume size` (baseline performance) and can burst up to 3,000 IOPS by depleting the bucket.
(+) All volumes get an initial 5.4 millions IO Credit, 30 Minutes @ 3,000 IOPS. Great for boots and initial workloads.
(+) Up to maximum for GP2 of 16,000 IO Credit per second, if the volumes is larger than 1,000 GB - baseline is above burst. Credit system isnt used and you always achieve baseline.
![[Pasted image 20240109174853.png]]

==> Great for boot volumes, low-latency, interactive apps, dev & test.
![[Pasted image 20240109175131.png]]

###### 2. EBS Volume Types Provisioned IOPS SSD (io1/2)
###### 3. EBS Volume Types - Hard Disk Drive (HDD)-based
![[Pasted image 20240109175605.png]]

###### 4. Instance Store Volumes
(+) Block Storage devices, physically connected one EC2 Host, instances on that host can access them, and offer the highest storage performance available in AWS.
(+) Included in instance price.
(+) It has to be `attached at launch`, and can not be attached after launch.
![[Pasted image 20240109175917.png]]

(+) If an instance moves between hosts, any data on the attached instance store volume would be lost as they would be given a new blank volume.
(+) More IOPS and Throughput vs EBS

`Note`
(+) Local on EC2 Host
(+) Add at launch Only
(+) Lost on instance move, resize or hardware failure
(+) High performance
(+) Included in the price of the instance, better to use it or waste it.
(+) It is `Temporary`, dont use it for persistent storage of data.

#### E. EBS Snapshots
(+) Snapshots are incremental volume copies to S3, as it is copied to S3, it is `region-resilence`.
(+) The first is a full copy of data on the volume not the full data limit of the storage.
(+) Future snaps are `incremental`, meaning the differnece between the current  data and the future data. For other backup system, using `incremental backup` presents a risk of if losing the incremental backup, the rest of the backup data from that point onward would also be lost. However, EBS's incremental backup can also be thought of as self-sufficient as losing an incremental backup would not affect the rest of the backup.
(+) Volumes can be created (restored) from snapshots, and can be copied to another region.
![[Pasted image 20240109181409.png]]
(+) New EBS volume = `full performance immediately`. Snaps `restore lazily` meaning that it will fetch the data gradually. But we can requested blocks are fetched immediately by forcing a read of all data.
(+) `Fast Snapshot Restore (FSR)` enables to restore immediately, up to 50 snaps per region.
(+) Snapshot consumption and billing are GB-per month but this is billed per `used data` not the `allocated data` meaning that if we used 10GB out of 40GB EBS and we create a snap for that EBS we will only be billed for 10GB. And in the future if we changed 4GB of that 10GB data, and create another snaps, we will only be charged for 4GB. Charged for `used data and changed data` not the `allocated data`.

#### F. EBS Encryption


