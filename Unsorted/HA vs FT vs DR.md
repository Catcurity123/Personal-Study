#### High-Availability (HA)
Aims to `ensure` an agreed level of operation performance, usually uptime, for a higher than normal period.

==> Meaning that should a service fails, another one can be created and worked in a timely manner. 

`99.9%` = 8.77 hours p/year of downtime.
`99.999%` = 5.26 minutes p/year downtime.

>[!Note] So High Availability is
>Keeping the system `operational`, `fast or automatic recovery` of system. `It has nothing to do with user's experience`, although user's experience is a perks of HA system

#### Fault-Tolerance (FT)
Property that enables a system to `continue operating properly in the event of failure` of some (one or more faults within) of its component.

==> Meaning that it has to be operational during fault time without affecting the user.

>[!Warning] For some systems, HA is not enough
>During downtime of critical system, even 1 minute of unoperational can be fatal to the system. FT on the other hand means that the system can work through downtime without unoperational period

Therefore, `HA means maximizing uptime, FT means coping with downtime without afftecting the system`.

Building FT while HA is needed means wasting money and building HA while FT is required means fatal downtime to system and human ife.

#### Disaster Recovery (DR)
A set of policies, tools and procedures to enable the recovery or continuation of vital technology infrastructre and systems following a natural or human-induced disaster.

=> Meaning that when HA and FT doesnot work, DR is needed.

![[Pasted image 20231105181940.png]]

![[Pasted image 20231105182104.png]]