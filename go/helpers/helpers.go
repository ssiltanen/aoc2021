package helpers

import (
	"io/ioutil"
	"strconv"
	"strings"
)

func ReadFile(file string) []string {
	buff, _ := ioutil.ReadFile(file)
	return strings.Split(string(buff), "\n")
}

func ToInt(str string) int {
	var val, _ = strconv.Atoi(str)
	return val
}
