@echo off
..\simple < t1_input.txt > test_output.txt
fc t1_output.txt  test_output.txt

..\simple < t2_input.txt > test_output.txt
fc t2_output.txt  test_output.txt

..\simple < t3_input.txt > test_output.txt
fc t3_output.txt  test_output.txt

..\simple < t4_input.txt > test_output.txt
fc t4_output.txt  test_output.txt

..\simple < t5_input.txt > test_output.txt
fc t5_output.txt  test_output.txt

Del test_output.txt

