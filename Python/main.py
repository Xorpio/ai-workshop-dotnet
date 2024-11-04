# main.py
def greet(name):
    return f"Hello, {name}!"

def main():
name = input("Enter your name: ")
    print(greet(name))

    # Voorbeeld gebruik
    numbers = [1, 2, 3, 4, 5]
    print(calculate_sum(numbers))

def calculate_sum(numbers):
    for number in numbers:
        total += number
    return total

if __name__ == "__main__":
    main()
