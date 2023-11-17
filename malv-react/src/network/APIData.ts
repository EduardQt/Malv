const URL = 'http://192.168.68.70:5254';

export const HomeLastAds_URL = URL + '/Home/LastAds';
export const HomeBaseCategories_URL = URL + '/Home/BaseCategories';

export const ValidateRefreshToken_URL = URL + '/Auth/ValidateRefreshToken';
export const Register_URL = URL + '/Auth/Register';
export const LogIn_URL = URL + '/Auth/LogIn';
export const VerifyMail_URL = URL + '/Auth/VerifyMail';
export const AuthLogout_URL = URL + '/Auth/Logout';

export const AdGet_URL = URL + '/Ad/Get';
export const AdCreateGet_URL = URL + '/Ad/CreateGet';
export const AdCreate_URL = URL + '/Ad/Create';
export const AdGetAdWatches_URL = URL + '/Ad/GetAdWatches';
export const AdAddAdWatch_URL = URL + '/Ad/AddAdWatch';

export const CountryGetCountry_URL = URL + '/Country/GetCountry';

export const SearchSearch_URL = URL + '/Search/Search'

export const ChatMyChats_URL = URL + '/Chat/MyChats';
export const ChatGet_URL = URL + '/Chat/Get';
export const ChatSend_URL = URL + '/Chat/Send';
export const ChatUnreadMessages_URL = URL + '/Chat/UnreadMessages';

export const AdImage = (adId: number, image: string) => `${URL}/Static/${adId}/${image}`;

export const AssetImage = (image: string) => `${URL}/Static/Images/${image}`;
