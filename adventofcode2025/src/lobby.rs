use crate::data_provider;
use crate::data_provider::get_data_when_lines;
use std::cmp::Reverse;
use std::iter::Cycle;
use std::num::NonZeroUsize;
use std::ops::{Add, Deref};
use std::process::id;

pub fn run_part_one(data: &str) -> i64 {
    let data_vector = data_provider::get_data_when_lines(data);
    let mut sum = 0;
    for data in data_vector {
        let set = find_closest_to_9(&data);
        if let Some((idx, c)) = set {
            let mut left_digit = -1;
            let mut right_digit = -1;

            if idx != 0 {
                let left_set: String = data[0..idx].to_string();
                let left_char = find_closest_to_9(&left_set);
                if let Some((idx, c1)) = left_char {
                    left_digit = format!("{c1}{c}").parse().unwrap();
                }
            }

            if idx < data.len() - 1 {
                let right_set: String = data[(idx + 1)..].to_string();
                let right_char = find_closest_to_9(&right_set);
                if let Some((idx, c2)) = right_char {
                    right_digit = format!("{c}{c2}").parse().unwrap();
                }
            }

            if left_digit > right_digit {
                sum += left_digit;
                continue;
            }
            sum += right_digit;
        }
    }
    println!("Result for lobby part one is {sum}");
    sum
}

fn find_closest_to_9(data: &String) -> Option<(usize, char)> {
    let set = data
        .chars()
        .enumerate()
        .min_by_key(|(idx, c)| {
            let d = c.to_string().parse::<i32>().unwrap();
            ((9 - d).abs(), *idx as i32)
        })
        .map(|(idx, c)| (idx, c));
    set
}

pub fn run_part_two(data: &str) -> i64 {
    let data_vector = data_provider::get_data_when_lines(data);
    let mut sum = 0;

    for line in data_vector {
        if let Some(best) = max_subsequence_of_len(&line, 12) {
            let value: i64 = best.parse().unwrap();
            sum += value;
        }
    }

    println!("\nResult for lobby part two is {sum}");
    sum
}

fn max_subsequence_of_len(s: &str, k: usize) -> Option<String> {
    let n = s.len();
    if n < k {
        return None;
    }

    let mut to_remove = n - k;
    let mut res: Vec<char> = Vec::with_capacity(n);

    for ch in s.chars() {
        while to_remove > 0 && !res.is_empty() && res.last().unwrap() < &ch {
            res.pop();
            to_remove -= 1;
        }
        res.push(ch);
    }

    if res.len() < k {
        return None;
    }
    res.truncate(k);
    Some(res.into_iter().collect())
}

fn lobby_test() {
    let values = "987654321111111\n811111111111119\n234234234234278\n818181911112111";
    let values = values.trim();
    let data = data_provider::get_data_when_lines(values);

    assert_eq!("811111111111119", data[1]);
    assert_eq!(357, run_part_one(values));
    assert_eq!(3121910778619, run_part_two(values));
}
