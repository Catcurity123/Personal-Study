#### A. CloudTrail Architecture
(+) Logs API calls/activities as a `CloudTrail Event`, this is stored by default for 90 days in `Event History` (not S3 or Cloudwatch, only Trails stores data in S3 or CloudWatch).
(+) To customize the service, we need to create 1 or more `Trails`
(+) `Management Events` and `Data Events`.

(+) `Trails` is the configurational unit of `CloudTrail`, once `Trails` is enabled, `Management Events and Data Events` is stored in `S3` for unlimited time.
	==> Apart from that `Trails` can export the data to `Cloudwatch Logs`.

==> `CloudTrail` is **NOT REALTIME**, there is a delay of 15 minutes on average.

#### B. Cloudtrail Pricing
(+) CloudTrail for 90 days in the `Event History` is free
(+) One Region for each AWS Account can create 1 copy of `Management Event` for free, additional copies is charged `$2.00 per 100,000 events`.
(+) Data Events is charged `$0.10 per 100.000 events`.

