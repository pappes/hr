@echo off
set app="""C:\Users\dj\Documents\GitHub\hr\tutorials\t1 - arrays left rotation\project\ConsoleApp\t1\t1\bin\Debug\t1.exe"""
%app% < t1_input.txt > test_output.txt
fc /w t1_output.txt  test_output.txt

%app% < t2_input.txt > test_output.txt
fc /w t2_output.txt  test_output.txt

%app% < t3_input.txt > test_output.txt
fc /w t3_output.txt  test_output.txt

%app% < t4_input.txt > test_output.txt
fc /w t4_output.txt  test_output.txt

rem %app% < t5_input.txt > test_output.txt
rem fc t5_output.txt  test_output.txt

Del test_output.txt

