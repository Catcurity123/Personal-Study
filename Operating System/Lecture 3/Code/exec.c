#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <wait.h>
#include <string.h>

int main (int argc, char *argv[]){
    printf("Hello world (PID:%d)\n", (int) getpid());
    int rc = fork();
    if (rc < 0){
        fprintf(stderr, "fork failed\n");
        exit(1);
    } else if (rc == 0){
        printf("child (PID:%d)\n", (int) getpid());
        char *myargs[3];
        myargs[0] = strdup("wc");
        myargs[1] = strdup("exec.c");
        myargs[2] = NULL;
        execvp(myargs[0], myargs);
        printf("this shouldnt print out");
    } else {
        int rc_wait = wait(NULL);
        printf("parent of %d (rc_wait:%d) (PID:%d)\n", rc, rc_wait, (int) getpid());
    }
    return 0;
}