#### But why?
(+) `Xen` is the most popular example of paravirtualizard VMM
(+) `Paravirtualization` means the guest OS is modified to be amenable to virtualization, meaning changes to its source code was made for virtualization to happens, for example, privileged calls is replaced by hyper calls to the hypervisor for privileged access.
==> The `benefits` is that it has better performance than binary translation, the `disadvantages` is that this requires changes in OS's source code. 