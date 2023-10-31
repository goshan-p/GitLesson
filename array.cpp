#include <iostream>
#include <stdlib.h>

using namespace std;

int main() {

  int N = 0;
  int m = 0;
  int k = 0;

  cout << "N: ";
  cin >> N;
  cout << "m: ";
  cin >> m;
  cout << "k: ";
  cin >> k;

  int *two_dim_arr = (int *)calloc(sizeof(int), m * k);

  for (int i = 0; i < m * k; i++) {
    two_dim_arr[i] = N + i;
  }

  for (int i = 0; i < m; i++) {
    for (int j = 0; j < k; j++) {
      cout << two_dim_arr[i * k + j] << " ";
    }
    cout << endl;
  }

  free(two_dim_arr);

  return 0;
}