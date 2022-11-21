/* eslint-disable */
const CheckLocalStorage = () => {
  for (var i = 0; i < localStorage.length; i++) {
    console.log(
      "Key: " +
        localStorage.key(i) +
        "\nValue: " +
        localStorage.getItem(localStorage.key(i))
    );
  }
};

const SaveSearchResult = (searchParams, searchResult) => {
  localStorage.setItem(
    JSON.stringify(searchParams),
    JSON.stringify(searchResult)
  );
};

const CheckSearchParams = (searchParams) => {
  if (localStorage.getItem(JSON.stringify(searchParams)) !== null) {
    return true;
  }
  return false;
};

const GetSearchResult = (searchParams) => {
  return JSON.parse(localStorage.getItem(JSON.stringify(searchParams)));
};

const ClearLocalStorage = () => {
  localStorage.clear();
};

export {
  CheckLocalStorage,
  SaveSearchResult,
  CheckSearchParams,
  ClearLocalStorage,
  GetSearchResult,
};
