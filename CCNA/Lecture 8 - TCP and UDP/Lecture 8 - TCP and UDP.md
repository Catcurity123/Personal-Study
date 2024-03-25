#### A. Functions of Layer 4
(+) Provides transparent transfer of data between end hosts
(+) Provides various services to applications:
	(-) Reliable data transfer
	(-) Error recovery
	(-) Data sequencing
	(-) Flow control
(+) Provides Layer 4 addressing (`port number`).
	==> Identify application layer protocol (TCP:80 => HTTP)
	==> Provides session multiplexing
	==> The following ranges have been designated by IANA
		(-) `Well-known` port numners: 0 -> 1023
		(-) `Registered` port numbers: 1024 -> 49151
		(-) `Ephemeral/private/ynamic` port number: 49152 -> 65535