#!/usr/bin/python
import os
import shutil
import sys
import platform

def copyfiles(args):

    # python  $(SolutionDir)copy.py $(TargetPath)  $(SolutionDir)bin\$(ConfigurationName) $(TargetDir)$(TargetName)  $(SolutionDir)bin\$(ConfigurationName)\pdb\ $(TargetPath) $(SolutionDir)bin
    # print parameter  & copy dll -> bin/debug or bin/release
    print('arg1:{0} arg2:{1}'.format(args[0], args[1]))

    if(os.path.exists(args[0])):
        print('cp {0} {1}'.format(args[0], args[1]))
        shutil.copy2(args[0], args[1])

    # print parameter & copy mdb(linux) or pdb(windows) -> bin/debug/pdb or bin/release/pdb
    print('arg3:{0} arg4:{1}'.format(args[2], args[3]))
    sysType = platform.system()
    #print('===currentpath:{0}'.format(sys.path))
    print(sysType)
    baseName = os.path.basename(args[2])  # get filename
    print('baseName:{0}'.format(baseName))
    sourceFile = args[2]

    if sysType == 'Linux':
        if baseName == 'MESReportServer':
            sourceFile = '{0}.exe.mdb'.format(sourceFile)
        else:
            sourceFile = '{0}.dll.mdb'.format(sourceFile)
    elif sysType == 'Windows':
        sourceFile = '{0}.pdb'.format(sourceFile)

    if(os.path.exists(sourceFile)):
        print('cp {0} {1}'.format(sourceFile, args[3]))
        shutil.copy2(sourceFile, args[3])

    # print parameter & copy dll ->$(Configuration)
    print('arg5:{0} arg6:{1}'.format(args[4], args[5]))
    target = '{0}$(Configuration)'.format(args[5])
    if os.path.exists(args[4]):
        print('cp {0} {1}'.format(args[4], target))
        shutil.copy2(args[4], target)

def copymdbs(args):
    '''
        copy mdb to MESReportServer directory on develop
    :param args:
    :return:
    '''
    sysType = platform.system()
    currPath=sys.path[0]
    target = os.path.join(currPath,'MESReportServer/bin/Debug')
    print('target:{0}'.format(target))
    baseName = os.path.basename(args[2])
    if baseName == 'MESReportServer':
        return
    sourceFile = args[2]
    if sysType == 'Linux':
        if baseName == 'MESReportServer':
            sourceFile = '{0}.exe.mdb'.format(sourceFile)
        else:
            sourceFile = '{0}.dll.mdb'.format(sourceFile)
    elif sysType == 'Windows':
        sourceFile = '{0}.pdb'.format(sourceFile)

    if (os.path.exists(sourceFile)):
        print('cp {0} {1}'.format(sourceFile, target))
        shutil.copy2(sourceFile, target)
if __name__ == '__main__':
    args=[]
    for i in range(1,7):
        args.append(sys.argv[i])
    #print('---------')
    #print(args)
    copyfiles(args)
    copymdbs(args)