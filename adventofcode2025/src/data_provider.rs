use std::fs;

pub fn load_data_from_file(path: &str) -> String {
    let contents = fs::read_to_string(path).expect("Something went wrong reading the file");
    contents
}

pub fn get_data_when_lines(string_data: &str) -> Vec<String> {
    let result: Vec<String> = string_data.lines().map(|s| s.trim().to_string()).collect();
    result
}

pub fn get_data_when_comma_separated(data: &str) -> Vec<String> {
    let result: Vec<String> = data
        .split(",")
        .map(|s| s.trim())
        .filter(|s| !s.is_empty())
        .map(String::from)
        .collect();
    result
}
