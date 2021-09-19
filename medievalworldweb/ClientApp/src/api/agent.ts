import axios, { AxiosResponse } from 'axios';
import { toast } from 'react-toastify';
import { IFightRequestDto, IUserDetail } from '../model/interfaces';

axios.defaults.baseURL = "/";
const responseBody = (response: AxiosResponse) => response.data;
axios.interceptors.request.use((config) => {
const token = window.localStorage.getItem("jwt");
if(token)
config.headers.Authorization = `Bearer ${token}`;
return config;
},error =>{

})
axios.interceptors.response.use(undefined, (error) => {
    if (error.message === 'Network Error' && !error.response) {
        toast.error("Network error - API is not running!!")
    }
    throw error.response;
});

const requests = {
    get: (url: string) => axios.get(url).then(responseBody),
    post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
    update: (url: string, body: {}) => axios.put(url, body).then(responseBody),
    delete: (url: string) => axios.delete(url).then(responseBody)
};


const FightDetailsApi = {
    getFightTally: (userId:number) => requests.get(`/Fight/Fighttally/${userId}`),
    getFightDetail: () => requests.get("/Fight/Fightdetail"),
    createFight: (fightRequest: IFightRequestDto) => requests.post('/Fight', fightRequest),
    getFighters:() => requests.get('Fight/Fighters'),
    getSelectedFighters:() => requests.get('Fight/SelectedFighters'),
    reset:(userId:number) => requests.get(`Fight/Reset/${userId}`)
};

const UserDetailsApi = {
    login:(userDetail:IUserDetail) => requests.post('/auth/login',userDetail),
    register:(userDetail:IUserDetail) => requests.post('/auth/register',userDetail)
}

export {
    FightDetailsApi,UserDetailsApi
}