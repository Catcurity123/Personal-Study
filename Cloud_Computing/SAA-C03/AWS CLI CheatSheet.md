#### AWS General
(+) `aws-configure`: To configure `Access-Key`, `Secreet-Access-Key`

(+) `aws sts get-caller-identity`

(+) `aws iam get-user`: This will get your own user

(+) Configuration file is stored at `~/.aws/config`



#### AWS S3
(+) `aws s3 ls`: List S3 bucket
	(-) `aws s3 mb s3://<bucket name>`: To make bucket
	(-) `aws s3 cp s3://<bucket name>/<filename> <filename on local>`: download file from aws s3



#### AWS EC2
(+) `aws ec2 describe-instances`


#### AWS Role and Profile

### ROLE ASSUMING USING CLI
###### 1. Create Trust Policy and assign it to a role
(+) Create an IAM Role by creating a `trust policy` and assign the trust policy to the according role name
```
{ "Version": "2012-10-17", "Statement": [ { "Effect": "Allow", "Principal": {"Service": "ec2.amazonaws.com"}, "Action": "sts:AssumeRole" } ] }
```

Then `aws iam create-role --role-name DEV_ROLE --assume-role-policy-document file://trust_policy_ec2.json`



###### 2. Create Instance Profile and Attach Role to an EC2 Instance
(+) Write a `Permission Policy` and then assign it to the designated `policy name`.

```
{ "Version": "2012-10-17", "Statement": [ { "Sid": "AllowUserToSeeBucketListInTheConsole", "Action": ["s3:ListAllMyBuckets", "s3:GetBucketLocation"], "Effect": "Allow", "Resource": ["arn:aws:s3:::*"] }, { "Effect": "Allow", "Action": [ "s3:Get*", "s3:List*" ], "Resource": [ "arn:aws:s3:::<DEV_S3_BUCKET_NAME>/*", "arn:aws:s3:::<DEV_S3_BUCKET_NAME>" ] } ] }
```

Then `aws iam create-policy --policy-name DevS3ReadAccess --policy-document file://dev_s3_read_access.json`

(+) After that we must create an `instance profile`, by attaching the `permission policy` to the role name (with the `trust policy`) and then create a profile and add the role to the profile 

`aws iam attach-role-policy --role-name DEV_ROLE --policy-arn `

`aws iam create-instance-profile --instance-profile-name DEV_PROFILE`

`aws iam add-role-to-instance-profile --instance-profile-name DEV_PROFILE --role-name DEV_ROLE`

![[Pasted image 20240227134226.png]]

###### 3. Attach the profile to an instance
`aws ec2 associate-iam-instance-profile --instance-id <LAB_WEB_SERVER_INSTANCE_ID> --iam-instance-profile Name="DEV_PROFILE"`


(+) `aws iam list-policies --scope Local`: list local policies
(+) `aws iam list-roles`: list roles
(+) `aws iam list-attached-role-policies --role-name DEV-ROLE`: list attached policies of a role name
(+) `aws iam get-instance-profile --instance-profile-name DEV_PROFILE`:
(+) `aws ec2 describe-instances --instance-ids <LAB_WEB_SERVER_INSTANCE_ID>`: describe an instance