#### A. Cloudwatch Logs
(+) `Public Service` - usable from AWS or on-premises
(+) `Store, Monitor and Access` logging data
(+) `AWS Integrations` - EC2, VPC Flow Logs, Lambda, Cloudtrail, R53 and more,...
(+) Can generate metrics based on logs - `metric filter`.

#### B. Cloudwatch Architecture
(+) `Logging Sources`: The sources of logging for Cloudwatch, this coud be  AWS service, mobile server, database, or external API
(+) `Logging Sources` will produce data that is recorded by Cloudwatch as `Log Events`
(+) `Log events` from one source is stored as `Log Stream`
(+) A group of multiple `Log Streams` is called `Log Group`
(+) `Log group` is the organizational unit for Cloudwatch settings, these inclde: `Retention and Permissions`. We can also applied `Metric Filter` to get `Metric` from `Log Group` to create an `Alarm`.
![[Pasted image 20240103134210.png]]






