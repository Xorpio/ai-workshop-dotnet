package main

import (
    "fmt"
    "os"
)

func main() {
    fileName := "example.txt"
    numbers := [5]int{1, 2, 3, 4, 5}

    file, err := os.Open(fileName)
    if err != nil {
        fmt.Println("Error opening file:", err)
        return
    }
    defer file.Close()

    fmt.Println("A number is:", numbers[5])

    // Lees de inhoud van het bestand
    buffer := make([]byte, 1024)
    n, err := file.Read(buffer)
    if err != nil {
        fmt.Println("Error reading file:", err)
        return
    }

    fmt.Println("File content:", string(buffer[:n]))
}
