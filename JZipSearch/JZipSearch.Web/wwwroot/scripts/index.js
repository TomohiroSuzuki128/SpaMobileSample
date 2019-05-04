var app = new Vue({
    el: '#app',
    data:{
        zipCode: '',
        prefecture: '13',
        prefectureList:[],
        address: '',
        addressList:[],
        isInAppBrowser:false
        },
    created: function(){
        axios.get("./Api/Prefectures")
            .then((response) => {
                this.prefectureList = response.data;
                this.prefecture = '13';
            });
    },
    methods:{
        handleZipCodeToAddressClick: function(event){
            this.addressList = [];
            axios.get("./Api/ZipCodeToAddress?q="+this.zipCode)
                .then((response) => {
                    this.addressList = response.data;
                    for (let address of this.addressList) {
                        let addressText = address.prefecture + address.city + address.machi;
                        let url = "https://www.bing.com/search?q="+addressText;
                        address.url = url;
                        address.target = (this.isInAppBrowser ? "_self" : "_blank");
                    }
                });
        },
        handleAddressToZipCodeClick: function (event){
            this.addressList = [];
            axios.get("./Api/AddressToZipCode?pref="+this.prefecture+"&addr="+this.address)
                .then((response) => {
                    this.addressList = response.data;
                    for (let address of this.addressList) {
                        let addressText = address.prefecture + address.city + address.machi;
                        let url = "https://www.bing.com/search?q="+addressText;
                        address.url = url;
                        address.target = (this.isInAppBrowser ? "_self" : "_blank");
                    }
                });
        }
    }
});

var topbox = document.getElementById('topbox');
var listbox = document.getElementById('listbox');
listbox.style.top = topbox.clientHeight + "px";
