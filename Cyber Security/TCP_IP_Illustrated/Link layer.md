# 3 Link Layer 

## 3.1 Ethernet and the IEEE 802 LAN/MAN Standards

The term Ethernet generally refers to a set of standards first published in 1980 and 
revised in 1982 by Digital Equipment Corp., Intel Corp., and Xerox Corp.

Because multiple stations share the same network, this standard includes a 
distributed algorithm implemented in each Ethernet network interface that controls when a station gets to send data it has. The particular method, known as 
**carrier sense, multiple access with collision detection (CSMA/CD)**, mediates which 
computers can access the shared medium (cable) without any other special agreement or synchronization.


**How CSMA/CD actually works?**
---

**1.** Carrier Sense: before transmitting data, device first checks whether the medium is free or not. **This is done by checking the state of the carrier signal on the communication medium**.

**2.** Multiple Access: Multiple devices share the same communication medium, so each device must wait for its turn to transmit data.

**3.** Collision Detection: If two devices begin transmitting data at the same time, a collision occurs. Each device will then send a "jam signal" to signal that a collision has occured. The devices then wait for a random amount of time before trying again.

**4.** Backoff algorithm: After a collision, the amoun of time is determined by a backoff algorithm that uses **a random number generator** to choose a waiting time between 0 and 2^k - 1 time slots, where k is the number of collision retries.

**5.** Exponential Backoff:  The backoff algorithm uses an exponential backoff strategy to reduce the likelihood of collisions. That is, if a collision occurs, the waiting time increase exponentially.

---


By the early 1990s, the shared cable had largely been replaced by twisted-pair wiring, contention-based MAC protocols have become less popular. Instead, the wiring between each LAN station is often not shared but instead provides a dedicated electrical path in a star topology, which is accomodated by Ethernet Switch.

![EtherSw](./Picture/EthernetSwitch.png)

### 3.2.1 The Ethernet Frame Format

The Ethernet frame consists of the following:

**1. Preamble and Start Frame Delimiter (SFD)**: 

**-** The Preamble is a 7-byte length to tell the receiver that an Ethernet frame is about to be sent. Each byte of this area is 1010 1010 or 0xAA in hexa. 

```
10101010 10101010 10101010 10101010 10101010 10101010
10101010

or 

AA AA AA AA AA AA AA
```

**-** The Start Frame Delimiter (SFD) is a 1-byte length to tell that the frame start right after it. The format of SFD is 1010 1011 or 0xAB in hexa.

```
1010 1011 or AB 
```

**Preamble and SFD** are important as it is the notification for receiving device to lock the byte stream and acknowledge the incoming frame. Without it, the receving device won't be able to recognize the Ethernet Frame at all.

Preamble and SFD are not a part of Ethernet Frame and will be discarded once the deliverance of Ethernet Frame is completed.

**2. Destination (DST) and Source (SRC) Address**:

DST and SRC contains the destination MAC address and the source MAC address. Each of them is 48-bit long or 6-byte long which is the same size of a MAC address.


**3. The Type/Length field**

The Type/Length field is a 2-byte (16-bit) field that is in the Ethernet Frame to give a head-up of what is in the data section of the frame. The field is used to specify the type of protocol being carried in the payload of the frame, or the length of the payload, depending on the value of the field. Therefore, there are 2 use cases of this field:

**-** If the value of the Type/Length field is greater than or equal to 0x600 (1536 decimal), it indicates that the payload of the Ethernet frame contains a protocol data unit (PDU) of a higher-layer network protocol, such as IP or ARP.

**=>** For example, IPv4 is 0x0800 is IPv4, 0x86DD is IPv6, 0x0806 is ARP.

**-** If the value of the Type/Length field is less than 0x600, it indicates that the payload of the Ethernet Frame is less than 1500 bytes, and it will interprete this field as a Length field. In this case, the payload may contain other types of data or information that do not require a higher-layer protocol.

**=>** Some industrial control systems use proprietary protocols to communicate between devices on a local network. These protocols may be designed to operate at a lower level of the network than the standard IP-based protocols. An example could be 0x200 or any other size that is less than 0x600.

**NOTE**

If the size of the Type field is 0x8100 (32768 decimal), it indicates that the frame is using a specilized Ethernet protocol called IEEE 802.1Q, which is also known as VLAN tagging.

In this case, two additional bytes called Tag Control Information (TCI) field are added after the Type field, TCI field is divided into two parts, the Priority Code Point (PCP) field and the VLAN Identifier (VID) field:

**-** PCP field is a 3-bit field that is used to indicate the priority of the frame, this is to ensure that high-priority traffic is givven precedence over lower-priority traffic. Typically, time-sensitive or mission-critical traffic such as real-time video or voice communication, financial transactions are considered high priority.

**-** VID field is a 12-bit field that is used to identify the VLAN to which the frame belongs. When a frame is transmitted, the VID field is set to the VLAN ID of the VLAN to which the frame belongs.

**-** There is also a CFI field, which is 1 byte long, this used to be used for compatibility with older networking equipment but nowadays it is not used anymore.

**4. Upper-Layer Protocol Payload**

This is the area where higher-layer PDUs such as IP datagrams are placed.Tradditionally, the payload area for Ethernet has always been 1500 bytes, representing the MTU for Ethernet.

**5. Frame Check Sequence/Cyclic Redundancy Check (CRC)**

The fianl field of the Ethernet frame format provides an integrity check on the frame. It is 32 bits and sometimes is known as IEEE/ANSI standard.

FCS field is a 4-byte field locaetd at the end of the Ethernet Frame, used by the receiving device to verify the integrity of the Ethernet Frame.

There are 4 components in this process, **the message (in binary), the generator polynomial, the padding bits, and the CRC value (or the remainder of this division)**, the process are as follows:

**Suppose we want to transmit the 6-bit message 100100 using an n-bit generator polynomial**

**1.** Find the length of the divisor `L`, this divisor is the binary representation of a polynomial.

```
For example, x^3 + x^2 + 1 is 1101 in binary.
```

**2.** We then need to append `L-1` 0s bit to the original message

```
In our example, 100100 => 100100000

This needs to be done so as to ensure that the CRC calculation includes all possible combinations of the message and the divisor. 

If there is no padding the remainder would not be used.
```

**3.** Perform binary division operation for the polynomial and the message. The CRC value (or the remainder must be L-1 bits).

```
111101 => quotient
---------
100100000 => message (M)
1101 => polynomial (L)
----
 1000
 1101
 ----
  1010
  1101
  ----
   1110
   1101
   ----
    0110
    0000
    ----
     1100
     1101
     ----
      001 => remainder (L-1)

=> Finally, we replace the padded 0s with the remainder making our message 100100001.
=> The number of step is M-L+1, in this example, 6 steps = 9 bits message - 4 bits poly.
```

```
Upon receiving the receiver will XoR the message again to check for its integrity

111101 => quotient
------
100100001 => message (M)
1101 => polynomial (L) the receiver know this as it is universal
----
 1000
 1101
 ----
  1010
  1101
  ----
   1110
   1101
   ----
    0110
    0000
    ----
     1101
     1101
     ----
      000 => If the remainder at the receiving end is 0s then there is no modification of the message.
```

**Some noteworthy point**

**3.1** Padded 0s for the message = L - 1, where L is the length of the binary polynomial.

**3.2** The The remainder of the division is also L - 1 as we will replace the 0s with the remainder.

**3.3** Number of steps, or division steps, is M - L + 1, where M is the bits of the message, L is the bits of the polynomial.
s

**3.4** This algorithm is done via the nature of XOR gate:

```
A XOR B = C => C XOR B = A 
101 XOR 111 = 010 => 010 XOR 111 = 101
```
Therefore, the message XORed at the receiving end must be 0s as:

```  
   (Message + 0s) XOR polynomial = (Message + remainder) at the sender
=> (Message + remainder) XOR polynomial = (Message + 0s) at the receiver
```

Therefore, with all of its components, an Ethernet Frame is 64-byte long minimum and 1518-byte long maximum.

This is a sort of trade-off: 

If a frame contains an error, only 1.5KB is needed to be retransmitted to repair the problem.

On the other hand, the size limits the MTU to no more than 1500 bytes. In order to send larger message, multiple frames are required (e.g., 64KB, a common larger size used with TCP/IP networks, would require at least 44 frames.)

