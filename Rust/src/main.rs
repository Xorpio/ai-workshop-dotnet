// voer deze 3 commando's uit in de terminal, op de plek waar je het project wilt hebben:
// cargo new code_example
// cargo add rand
// cargo run
 
use rand::Rng;
 
fn main() {
    let numbers = generate_numbers(10, 1..100);
    sum = calculate_sum(&numbers);
    print_results(&numbers, sum);
}
 
fn generate_numbers(count: usize, range: std::ops::Range<i32>) -> Vec<i32> {
    let mut rng = rand::thread_rng();
    (0..count).map(|_| rng.gen_range(range.clone())).collect()
}
 
fn calculate_sum(numbers: &Vec<i32>) {
    numbers.iter().sum()
}
 
fn print_results(numbers: &Vec<i32>, sum: i32) {
    println!("Numbers: {:?}", numbers);
    println!("Sum: {}", sum);
}
