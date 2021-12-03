package main

import (
	"fmt"

	h "aoc/helpers"
)

func main() {
	data := h.ReadFile("../data/day01.txt")

	// Part 1
	var inc1, curr int = 0, h.ToInt(data[0])
	for i := 1; i < len(data); i++ {
		prev := curr
		curr = h.ToInt(data[i])
		if curr > prev {
			inc1++
		}
	}
	fmt.Println("Day1-1", inc1)

	// Part 2
	var inc, prev int = 0, 0
	for i := 2; i < len(data); i++ {
		a := h.ToInt(data[i])
		b := h.ToInt(data[i-1])
		c := h.ToInt(data[i-2])
		curr = a + b + c
		if curr > prev && prev != 0 {
			inc++
		}
		prev = curr
	}
	fmt.Println("Day1-2", inc)
}
