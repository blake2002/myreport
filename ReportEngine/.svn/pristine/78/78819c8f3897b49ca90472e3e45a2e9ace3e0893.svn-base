#!/bin/bash
echo "cp" $1 $2 #>> "/home/administrator/aa.log"
cp $1 $2
#sleep 10s
#$(SolutionDir)copy.sh $(TargetPath)  $(SolutionDir)bin\$(ConfigurationName) $(TargetDir)$(TargetName)  $(SolutionDir)bin\$(ConfigurationName)\pdb\ $(TargetPath) $(SolutionDir)bin
echo "\$3:" $3 "\$4:" $4
SYSTEM=`uname -s`    #获取操作系统类型，我本地是linux
if [ $SYSTEM = "Linux" ] ; then     #如果是linux的话打印linux字符串
	echo "Linux" 
	varTmp1=$1
	varTmp2=${varTmp1:0-19:19} # 从右边取出TargetName
	#echo "varTmp2" $varTmp2
	appName="MESReportServer.exe"	
	varTmp3=$3
	if [ $varTmp2 = $appName ] ; then 
		var3=${varTmp3}".exe.mdb" # $1  包含 $appName
		
	else
		var3=${varTmp3}".dll.mdb" # $1  包不含 $appName	
	fi
	#varTmp4=$4
	#var4=${varTmp4}".dll.mdb"
	echo "cp" $var3 $4
	cp $var3 $4
	cp $var3 $4
elif [ $SYSTEM = "FreeBSD" ] ; then   
	echo "FreeBSD" 
elif [ $SYSTEM = "Solaris" ] ; then 
	echo "Solaris" 
else 
	echo "Windows"
 	varTmp3=$3
	var3=${varTmp3}".pdb"
	echo "cp" $var3 $4
	cp $var3 $4
fi     #ifend

#sleep 10s
#echo "cp" $5 $6
val1=$6
val2=${val1}"\$(Configuration)"
echo "cp" $5 $val2
cp $5 $val2



# 用作本地开发使用 ，将mdb调试文件 复制到启动程序，以便调试
dllArray=("DesignerLib" "MES.PublicInterface" "ECharts" "HtmlTags" "NetSCADA.ReportEngine" "Host" "Loader" "MES.MESReportRun" "PMS.PMSChart" "PMS.PmsPublicData" "PMS.PMSPublicInfo" "PMS.ReportControls")
for val in ${dllArray[@]};do
	echo "\$val" $val
	val1=$3
	if [[ $val1 =~ $val ]] # 包含
	then
		val2=${val1//$val/"MESReportServer"}
		echo "\$val2" $val2
		if [ $val = "PMS.ReportControls" ] ; then 
			val2=${val2/"PmsReport"/"MESReportServer"}
			echo "\$val2" $val2
		fi
		val3=${val2%/*}
		echo "\$val3" $val3
		cp $var3 $val3
	fi
done
