#include<stdio.h>
void enqueue(int[],int*,int*,int);
int check(int,int[]);
int dequeue(int[],int*,int*);
void main()
{
    int adj[4][4]={{0,1,0,1}, {1,0,1,0}, {0,1,0,1}, {1,0,1,0}};
    int q[10], t[10], temp[3]={1,2,3};
    int i, j, fp=-1, rp=-1;
    printf("%d ",adj[0][0]);
    for(i=0;i<4;i++)
    {
        for(j=0;j<4;j++)
        {
            if(adj[i][j]==1)
            {
                if(check(j,temp))
                {
                    enqueue(q,&fp,&rp,j);
                }
            }
        }
    }
    for(i=0;i<3;i++)
    {
        printf("%d ",q[i]);
    }
}
void enqueue(int q[], int *fp, int *rp, int n)
{
    if((*fp)==-1)
    {
        (*fp)++;
    }
    (*rp)++;
    q[*rp]=n;
}
int check(int d, int t[3])
{
    int i;
    for(i=0;i<3;i++)
    {
        if(t[i]==d)
        {
            t[i]=-999;
            return 1;
        }
    }
    return 0;
}
int dequeue(int q[], int *fp, int *rp)
{
    int t;
    if(*fp == *rp)
    {
        t=q[*fp];
        return(t);
    }
    else
    {
        t=q[*fp];
        fp++;
        return(t);
    }
}
