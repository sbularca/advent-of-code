use crate::data_provider;

pub fn run_part_one(data: &str) -> i32 {
    let string_data = data_provider::get_data_when_lines(data);
    let mut current_index = 50;
    let mut result = 0;
    for line in string_data {
        if line.is_empty() {
            continue;
        }

        let dir = line.as_bytes()[0] as char;
        let num: i32 = line[1..].parse().unwrap();
        match dir {
            'L' => current_index -= num,
            'R' => current_index += num,
            _ => continue, // ignore unexpected lines
        }

        current_index = ((current_index % 100) + 100) % 100;

        if current_index == 0 {
            result += 1;
        }
    }

    println!("\nPart One Result {}", result);
    result
}

pub fn run_part_two(data: &str) -> i32 {
    let mut current_index: i32 = 50;
    let mut result: i32 = 0;
    let string_data = data_provider::get_data_when_lines(data);
    for line in string_data {
        if line.is_empty() {
            continue;
        }

        let dir = line.as_bytes()[0] as char;
        let num: i32 = line[1..].parse().unwrap();
        let old = current_index;
        let passes = match dir {
            'R' => {
                let end_linear = old + num;
                let visits = end_linear / 100;
                current_index = end_linear.rem_euclid(100);
                visits
            }
            'L' => {
                let end_linear = old - num;
                let visits = if old == 0 {
                    num / 100
                } else if num >= old {
                    (num - old) / 100 + 1
                } else {
                    0
                };
                current_index = end_linear.rem_euclid(100);
                visits
            }
            _ => continue,
        };

        result += passes;
    }

    println!("\nPart Two Result {}", result);
    result
}

#[test]
#[ignore]
fn check_result() {
    let values = "L68\nL30\nR48\nL5\nR60\nL55\nL1\nL99\nR14\nL82";

    assert_eq!(run_part_one(values), 3);
    assert_eq!(run_part_two(values), 6);
}
