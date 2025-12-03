use std::fs;

pub fn get_data_from_path(path: &str) -> Vec<String> {
    let contents = fs::read_to_string(path).expect("Something went wrong reading the file");
    let result: Vec<String> = contents.lines().map(|s| s.to_string()).collect();
    result
}

#[cfg(test)]
pub fn get_data_from_string(string_data: &str) -> Vec<String> {
    let result: Vec<String> = string_data.lines().map(|s| s.trim().to_string()).collect();
    result
}
